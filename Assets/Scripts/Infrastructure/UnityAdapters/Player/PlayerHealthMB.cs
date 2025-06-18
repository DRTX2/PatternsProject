using UnityEngine;
using Zenject;

public class PlayerHealthMB : MonoBehaviour, IDamageable, IHealable
{
    [Inject] private Player _player;

    private IAnimatorAdapter _animator;
    private IPhysicsAdapter _physics;

    [SerializeField] private float invincibilityDuration = 0.25f;
    [SerializeField] private float lockVelocityDuration = 0.1f;

    private float _invincibleTimer = 0f;
    private float _lockVelocityTimer = 0f;
    public bool _isAlive => _player.IsAlive;
    public bool CanHeal => _player.Health.CanHeal;
    public float CurrentHealth => _player.Health.Current;
    public float MaxHealth => _player.Health.Max;

    private void Awake()
    {
        _animator = new AnimatorAdapter(GetComponent<Animator>());
        _physics = new RigidbodyAdapter(GetComponent<Rigidbody2D>());
    }
    private void Update()
    {
        if (_invincibleTimer > 0 && (_invincibleTimer -= Time.deltaTime) <= 0)
            _player.SetInvincibility(false);

        /*if (_lockVelocityTimer > 0 && (_lockVelocityTimer -= Time.deltaTime) <= 0)
            _player.SetLockVelocity(false);*/
    }

    public bool ReceiveDamage(float amount, Vector2 knockback)
    {
        if (!_player.TakeDamage(amount)) return false;

        _animator.SetTrigger(AnimationStrings.hitTrigger);
        _physics.ApplyKnockback(knockback);

        _invincibleTimer = invincibilityDuration;
        _player.SetInvincibility(true);

        _lockVelocityTimer = lockVelocityDuration;
        //_player.SetLockVelocity(true);
        
        if (!_player.IsAlive) _animator.SetBool(AnimationStrings.isAlive, false);
        return true;
    }

    public bool Heal(float amount)
    {
        return _player.TryHeal(amount);
    }
}
