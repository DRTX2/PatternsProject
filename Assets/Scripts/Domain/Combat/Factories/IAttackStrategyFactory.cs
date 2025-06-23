/// <summary>
/// IAttackStrategyFactory is an interface that defines a factory for creating attack strategies.
/// Allows for the creation of different attack strategies based on the specified attack animation type.
/// </summary>
public interface IAttackStrategyFactory
{
    IAnimationAttackStrategy CreateStrategy(AttackAnimationType type);
}
