using System;

/// <summary>
/// Entidad de dominio pura para el enemigo Knight.
/// </summary>
public class KnightEnemy
{
    public enum Direction { Left, Right }

    public Health Health { get; }
    public bool IsAlive => Health.Current > 0;
    public bool IsInvincible { get; private set; }
    public bool CanAttack => IsAlive && AttackCooldown <= 0f;
    public bool CanMove { get; private set; } = true;
    public Direction Facing { get; private set; }

    public float AttackCooldown { get; private set; }
    public float WalkSpeed { get; } = 4f;
    public float WalkStopRate { get; } = 0.05f;

    public float HorizontalSpeed => Facing == Direction.Right ? WalkSpeed : -WalkSpeed;

    public KnightEnemy(float maxHealth, Direction initialDirection = Direction.Right)
    {
        Health = new Health(maxHealth);
        Facing = initialDirection;
        AttackCooldown = 0f;
        IsInvincible = false;
    }

    public void FlipDirection()
    {
        Facing = Facing == Direction.Left ? Direction.Right : Direction.Left;
    }

    public void SetAttackCooldown(float cooldown)
    {
        AttackCooldown = Math.Max(0f, cooldown);
    }

    public bool TakeDamage(float amount)
    {
        if (!IsAlive || IsInvincible) return false;
        Health.Reduce(amount);
        return true;
    }

    public void SetInvincibility(bool value) => IsInvincible = value;
    public void SetCanMove(bool value) => CanMove = value;
}
