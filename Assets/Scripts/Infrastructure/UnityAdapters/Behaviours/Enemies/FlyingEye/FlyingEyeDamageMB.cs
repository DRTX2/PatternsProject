using UnityEngine;
using Zenject;
/// <summary>
/// Damage behaviour for the Flying Eye enemy.
/// </summary>
public class FlyingEyeDamageMB : MonoBehaviour, IDamageBehaviour
{
    [Inject] private FlyingEyeEnemy _eye;
    [Inject] private CharacterEventBus _eventBus;

    private IAnimatorAdapter _animator;
    private IPhysicsAdapter _physics;

    [SerializeField] private float invincibilityDuration = 2f;
    private float _invincibleTimer = 0f;

    public float CurrentHealth => _eye.Health.Current;
    public float MaxHealth => _eye.Health.Max;
    public bool _isAlive => _eye.IsAlive;

    private void Awake()
    {
        _animator = new AnimatorAdapter(GetComponent<Animator>());
        _physics = new RigidbodyAdapter(GetComponent<Rigidbody2D>());
    }

    private void Update()
    {
        if (_invincibleTimer > 0f)
        {
            _invincibleTimer = Mathf.Max(_invincibleTimer - Time.deltaTime, 0f);
            if (_invincibleTimer <= 0f)
                _eye.SetInvincibility(false);
        }
    }

    public bool ReceiveDamage(float amount, Vector2 knockback)
    {
        if (!_eye.TakeDamage(amount)) return false;

        _animator.SetTrigger(AnimationStrings.hitTrigger);
        _physics.ApplyKnockback(knockback);

        _invincibleTimer = invincibilityDuration;
        _eye.SetInvincibility(true);

        if (!_eye.IsAlive)
        {
            _animator.SetBool(AnimationStrings.isAlive, false);
            _physics.SetGravityScale(2f);
        }

        _eventBus.DamageReceived.Notify(new DamageEvent { Character = gameObject, Amount = amount });
        return true;
    }
}