using System;

/// <summary>
/// Factory for creating attack strategies based on the specified attack animation type.
/// </summary>

public class AttackStrategyFactory : IAttackStrategyFactory
{
    private readonly IProjectileSpawner _projectileSpawner;
    public AttackStrategyFactory(IProjectileSpawner projectileSpawner)
    {
        _projectileSpawner = projectileSpawner;
    }
    public IAnimationAttackStrategy CreateStrategy(AttackAnimationType type)
    {
        return type switch
        {
            AttackAnimationType.Melee => new MeleeAttackStrategy(),
            AttackAnimationType.Ranged => new RangedAttackStrategy(_projectileSpawner),
            AttackAnimationType.Target => new TargetedAttackStrategy(),  
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}
