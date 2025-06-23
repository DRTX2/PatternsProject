/// <summary>
/// AttackUseCase is a use case that handles the attack logic for an entity.
/// This class encapsulates the attack behavior and the strategy used for attacking.
/// T represents the type of entity that can be attacked, which must implement IAttackableEntity.
/// </summary>
/// <typeparam name="T"></typeparam>
/// 
public class AttackUseCase<T> where T : IAttackableEntity
{
    private readonly IAttackBehaviour<T> _attacker;
    private readonly IAttackStrategyFactory _factory;

    public AttackUseCase(IAttackBehaviour<T> attacker, IAttackStrategyFactory factory)
    {
        _attacker = attacker;
        _factory = factory;
    }

    public void HandleAttack(AttackAnimationType type)
    {
        var strategy = _factory.CreateStrategy(type);
        _attacker.Attack(strategy);
    }
}