using System;

public class AttackStrategyFactory : IAttackStrategyFactory
{
    private readonly IProjectileSpawner _projectileSpawner;
    public AttackStrategyFactory(IProjectileSpawner projectileSpawner)
    {
        _projectileSpawner = projectileSpawner;
    }
    public IAttackCommnad CreateStrategy(AttackType type)
    {
        return type switch
        {
            AttackType.Melee => new MeleeAttackStrategy(),
            AttackType.Ranged => new RangedAttackStrategy(_projectileSpawner),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}
