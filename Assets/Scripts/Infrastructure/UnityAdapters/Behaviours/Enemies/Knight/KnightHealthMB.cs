using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class KnightHealthMB : MonoBehaviour, IDamageable
{
    [Inject] private KnightEnemy _knight;
    [Inject] private CharacterEventBus _eventBus;

    private IAnimatorAdapter _animator;
    private IPhysicsAdapter _physics;

    [SerializeField] private float invincibilityDuration = 0.25f;
    [SerializeField] private float lockVelocityDuration = 0.1f;

    private float _invincibleTimer = 0f;
    private float _lockVelocityTimer = 0f;

    private void Awake()
    {
        _animator = new AnimatorAdapter(GetComponent<Animator>());
        _physics = new RigidbodyAdapter(GetComponent<Rigidbody2D>());
    }

    private void Update()
    {
        if (_invincibleTimer > 0f)
        {
            _invincibleTimer -= Time.deltaTime;
            if (_invincibleTimer <= 0f)
            {
                _knight.SetInvincibility(false);
            }
        }
    }

    public bool IsAlive => _knight.IsAlive;
    public float CurrentHealth => _knight.Health.Current;
    public float MaxHealth => _knight.Health.Max;

    public bool _isAlive => throw new System.NotImplementedException();

    public bool ReceiveDamage(float amount, Vector2 knockback)
    {
        if (!_knight.TakeDamage(amount)) return false;

        _animator.SetTrigger(AnimationStrings.hitTrigger);
        _physics.ApplyKnockback(knockback);

        _invincibleTimer = invincibilityDuration;
        _knight.SetInvincibility(true);

        if (!_knight.IsAlive) _animator.SetBool(AnimationStrings.isAlive, false);

        _eventBus.DamageReceived.Notify(new DamageEvent { Character = gameObject, Amount = amount });
        return true;
    }
}
