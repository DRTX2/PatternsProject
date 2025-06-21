public interface IAttackStrategyFactory
{
    IAnimationAttackStrategy CreateStrategy(AttackAnimationType type);
}
