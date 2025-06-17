using System.Collections.Generic;
using System.Diagnostics;
public class AttackUseCase
{
    private readonly IAttacker _attacker;
    private readonly IAttackStrategyFactory _factory;

    public AttackUseCase(IAttacker attacker, IAttackStrategyFactory factory)
    {
        _attacker = attacker;
        _factory = factory;
    }

    public void HandleAttack(AttackType type)
    {
        var strategy = _factory.CreateStrategy(type);
        _attacker.Attack(strategy);
    }
}
