using UnityEngine;
using Zenject;

/// <summary>
/// Handles player damage behavior, including taking damage, applying knockback,
/// </summary>
public class PlayerDamageMB : MonoBehaviour, IDamageBehaviour
{
    [Inject] private Player _player;
    [Inject] private CharacterEventBus _eventBus;

    private IAnimatorAdapter _animator;
    private IPhysicsAdapter _physics;

    [SerializeField] private float invincibilityDuration = 0.25f;
    [SerializeField] private float lockVelocityDuration = 0.1f;

    private float _invincibleTimer = 0f;
    private float _lockVelocityTimer = 0f;

    public bool _isAlive => _player.IsAlive;
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
    }

    public bool ReceiveDamage(float amount, Vector2D knockback)
    {
        Debug.Log($"[DMG] Intentando aplicar {amount} → Vida antes: {_player.Health.Current}");

        if (!_player.TakeDamage(amount)) return false;
        
        _animator.SetTrigger(AnimationStrings.hitTrigger);
        _physics.ApplyKnockback(knockback);

        _invincibleTimer = invincibilityDuration;
        _player.SetInvincibility(true);
        _lockVelocityTimer = lockVelocityDuration;

        _eventBus.DamageReceived.Notify(new DamageEvent { Character = gameObject, Amount = amount });
        _eventBus.HealthChanged.Notify(new HealthChangedEvent { Character = gameObject, Current = CurrentHealth, Max = MaxHealth });

        if (!_player.IsAlive)
            _animator.SetBool(AnimationStrings.isAlive, false);
        Debug.Log($"[DMG] Vida después {_player.Health.Current}");
        return true;
    }
}
