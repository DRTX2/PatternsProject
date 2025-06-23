/// <summary>
/// RangedAttackStrategy is a class that implements the IAnimationAttackStrategy interface.
/// Uses a projectile spawner to handle ranged attacks in the game.
/// </summary>

public class RangedAttackStrategy : IAnimationAttackStrategy
{
    private readonly IProjectileSpawner _projectileSpawner;
    public RangedAttackStrategy(IProjectileSpawner projectileSpawner)
    {
        _projectileSpawner = projectileSpawner;
    }

    public void ExecuteAttack(IAnimatorAdapter animator)
    {
        animator.SetTrigger(AnimationStrings.rangedAttackTrigger);
        //_projectileSpawner.FireProjectile();
    }
}
