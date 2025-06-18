public interface IAttackStrategyFactory
{
    IAttackCommnad CreateStrategy(AttackType type);
}
