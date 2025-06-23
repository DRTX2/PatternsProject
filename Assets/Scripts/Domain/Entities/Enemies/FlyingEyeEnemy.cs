using System;
/// <summary>
/// FlyingEyeEnemy is a class representing an enemy entity that can fly, attack, and track the player.
/// </summary>
public class FlyingEyeEnemy : IMovableEntity, IAttackableEntity, IDamageableEntity, ITrakerEntity
{
    public Health Health { get; }
    public float AttackCooldown { get; private set; }
    public bool LockVelocityActive { get; private set; }
    public bool IsAttacking { get; private set; } = false;
    public bool HasTracker { get; private set; } = false;
    public bool IsInvincible { get; private set; }
    public bool IsAlive => Health.Current > 0;
    public float CurrentHealth => Health.Current;

    public float MaxHealth => Health.Max;


    public FlyingEyeEnemy(float maxHealth)
    {
        Health = new Health(maxHealth);
        AttackCooldown = 0f;
    }

    public bool TakeDamage(float amount)
    {
        if (!IsAlive || IsInvincible) return false;
        Health.Reduce(amount);
        return true;
    }

    public void SetInvincibility(bool value) => IsInvincible = value;
    public void SetLockVelocity(bool value) => LockVelocityActive = value;
    public void SetTracker(bool value) => HasTracker = value;
    public void SetAttackCooldown(float cooldown) => AttackCooldown = Math.Max(0f, cooldown);

    public bool CanMove(bool isGrounded, bool isTouchingWall)
    {
        return IsAlive && !LockVelocityActive;
    }

}