public class RangedAttackStrategy : IAttackCommnad
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
