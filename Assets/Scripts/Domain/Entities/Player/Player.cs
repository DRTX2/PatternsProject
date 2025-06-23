using System;
/// <summary>
/// Player class representing the player entity in the game.
/// Concrete implementation of a player that can move, attack, heal, take damage, jump, and run.
/// </summary>
public class Player : IMovableEntity, IAttackableEntity, IHealableEntity, IDamageableEntity, IJumpableEntity, IRunnableEntity
{
    public Health Health { get; }
    public float AttackCooldown { get; private set; }
    public bool LockVelocityActive { get; private set; }
    public bool IsAttacking { get; private set; }

    public bool IsRunning { get; private set; }
    public bool IsInvincible { get; private set; }
    public bool IsAlive => Health.Current > 0;

    public float CurrentHealth => Health.Current;

    public float MaxHealth => Health.Max;

    public bool IsJumping { get; private set; } = false;

    public int Score { get; set; }

    public float PositionX { get;  set; }
    public float PositionY { get; set; }

    public int EnemiesEliminated { get; private set; }

    public Player(float maxHealth, float currentHealth, float positionX, float positionY, int enemiesEliminated, int score)
    {
        Health = new Health(maxHealth);
        Health.ForceSetCurrent(currentHealth);
        PositionX = positionX;
        PositionY = positionY;
        EnemiesEliminated = enemiesEliminated;
        Score = score;
    }

    public bool TakeDamage(float amount)
    {
        if (!IsAlive || IsInvincible) return false;
        Health.Reduce(amount);
        return true;
    }

    public void SetRunning(bool running) => IsRunning = running;
    public void SetInvincibility(bool invincible) => IsInvincible = invincible;
    public void SetAttacking(bool attacking) => IsAttacking = attacking;
    public void SetLockVelocity(bool value) => LockVelocityActive = value;
    public void SetJumping(bool jumping) => IsJumping = jumping;

    public bool CanMove(bool isGrounded, bool isTouchingWall)
    {
        if (!IsAlive || LockVelocityActive) return false;
        if (IsAttacking && isGrounded) return false;
        if (!isGrounded && isTouchingWall) return false;
        return true;
    }

    public void SetAttackCooldown(float cooldown)
    {
        AttackCooldown = Math.Max(0f, cooldown);
    }

    public bool TryHeal(float amount)
    {
        if (!Health.CanHeal) return false;
        Health.Restore(amount);
        return true;
    }
}