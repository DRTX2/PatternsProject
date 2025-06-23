using System;
using UnityEngine;

/// <summary>
/// KnightEnemy represents a knight-type enemy in the game.
/// Allows for movement, attacking, taking damage, and tracking targets.
/// </summary>
public class KnightEnemy : IMovableEntity, IAttackableEntity, IDamageableEntity, ITrakerEntity
{
    public enum Direction { Left, Right }

    public Health Health { get; }
    public float AttackCooldown { get; private set; }
    public bool LockVelocityActive { get; private set; }
    public bool IsAttacking { get; private set; }
    public bool IsInvincible { get; private set; }
    public bool HasTracker { get; private set; } = false;
    public bool IsAlive => Health.Current > 0;
    public float CurrentHealth => Health.Current;

    public float MaxHealth => Health.Max;
    public Direction Facing { get; private set; }

    public float WalkSpeed { get; } = 4f;
    public float WalkStopRate { get; } = 0.05f;
    public float HorizontalSpeed => Facing == Direction.Right ? WalkSpeed : -WalkSpeed;

    public KnightEnemy(float maxHealth, Direction initialDirection = Direction.Right)
    {
        Health = new Health(maxHealth);
        Facing = initialDirection;
        AttackCooldown = 0f;
    }

    public void FlipDirection()
    {
        Facing = Facing == Direction.Left ? Direction.Right : Direction.Left;
    }

    public bool TakeDamage(float amount)
    {
        if (!IsAlive || IsInvincible) return false;
        Health.Reduce(amount);
        return true;
    }

    public void SetInvincibility(bool value) => IsInvincible = value;
    public void SetLockVelocity(bool value) => LockVelocityActive = value;
    public void SetAttacking(bool value) => IsAttacking = value;
    public void SetAttackCooldown(float cooldown) => AttackCooldown = Math.Max(0f, cooldown);
    public void SetTracker(bool value) => HasTracker = value;

    public bool CanMove(bool isGrounded, bool isTouchingWall)
    {
        if (!IsAlive || LockVelocityActive) return false;
        if (IsAttacking && isGrounded) return false;
        if (!isGrounded && isTouchingWall) return false;
        return true;
    }
}