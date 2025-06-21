using System;

public class FlyingEyeEnemy
{
    public Health Health { get; }
    public bool IsAlive => Health.Current > 0;
    public bool CanAttack => IsAlive && AttackCooldown <= 0f;
    public bool IsInvincible { get; private set; }
    public float AttackCooldown { get; private set; }

    public FlyingEyeEnemy(float maxHealth)
    {
        Health = new Health(maxHealth);
        AttackCooldown = 0f;
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
}
