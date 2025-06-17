public class Player
{
    public Health Health { get; private set; }
    public bool IsAlive => Health.Current > 0;
    public bool CanMove => IsAlive;
    public bool IsRunning { get; private set; }
    public bool IsInvincible { get; private set; }
    //public bool LockVelocity { get; private set; }

    public Player(float maxHealth)
    {
        Health = new Health(maxHealth);
    }
    
    public bool TakeDamage(float amount)
    {
        if (!IsAlive || IsInvincible) return false;
        Health.Reduce(amount);
        return true;
    }
    public bool TryHeal(float amount)
    {
        if (!Health.CanHeal) return false;
        Health.Restore(amount);
        return true;
    }

    public void SetRunning(bool running) => IsRunning = running;
    public void SetInvincibility(bool invicibility) => IsInvincible = invicibility;
    //public void SetLockVelocity(bool lockVelo) => LockVelocity = lockVelo;
}