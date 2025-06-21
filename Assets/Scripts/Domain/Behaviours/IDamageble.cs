public interface IDamageable
{
    float CurrentHealth { get; }
    float MaxHealth { get; }
    bool ReceiveDamage(float amount, UnityEngine.Vector2 knockback);
    bool _isAlive { get; }
}
