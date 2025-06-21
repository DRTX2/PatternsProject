using UnityEngine;
using Zenject;

public class FlyingEyeHealthMB : MonoBehaviour, IDamageable
{
    [Inject] private FlyingEyeEnemy _flyingEye;

    private IAnimatorAdapter _animator;
    private IPhysicsAdapter _physics;

    [SerializeField] private float invincibilityDuration = 2f;
    [SerializeField] private float lockVelocityDuration = 0.35f;

    private float _invincibleTimer = 0f;

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
            {
                _flyingEye.SetInvincibility(false);
            }
        }
    }

    public float CurrentHealth => _flyingEye.Health.Current;
    public float MaxHealth => _flyingEye.Health.Max;
    public bool _isAlive => _flyingEye.IsAlive;

    public bool ReceiveDamage(float amount, Vector2 knockback)
    {
        if (!_flyingEye.TakeDamage(amount)) return false;

        _animator.SetTrigger(AnimationStrings.hitTrigger);
        _physics.ApplyKnockback(knockback);

        _invincibleTimer = invincibilityDuration;
        _flyingEye.SetInvincibility(true);

        if (!_flyingEye.IsAlive)
        {
            _animator.SetBool(AnimationStrings.isAlive, false);
            // Lógica especial de muerte, como caída
            _physics.SetGravityScale(2f);
            Debug.Log("FlyingEye has died.");
        }

        CharacterEvents.OnDamageReceived?.Invoke(gameObject, amount);
        return true;
    }
}
