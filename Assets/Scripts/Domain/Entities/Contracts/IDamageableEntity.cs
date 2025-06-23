/// <summary>
/// Contract for entities that can take damage.
/// Defines methods and properties related to health, invincibility, and damage handling.
/// </summary>
public interface IDamageableEntity
{
    bool IsAlive { get; }
    bool IsInvincible { get; }
    void SetInvincibility(bool value);
    void SetLockVelocity(bool value);
    bool TakeDamage(float amount);
    float CurrentHealth { get; }
    float MaxHealth { get; }
}
