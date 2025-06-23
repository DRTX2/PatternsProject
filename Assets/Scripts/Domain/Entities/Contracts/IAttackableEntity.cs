/// <summary>
/// Contract for attackable entities.
/// Defines properties and methods related to attacking behavior.
/// </summary>

public interface IAttackableEntity
{
    bool IsAlive { get; }
    float AttackCooldown { get; }

    bool CanAttack => IsAlive && AttackCooldown <= 0f;

    void SetAttackCooldown(float cooldown);
}
