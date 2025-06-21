using UnityEngine;
public class HealthInteractionPresenter
{
    private readonly DamageUseCase _damageUseCase;
    private readonly HealUseCase _healUseCase;

    public HealthInteractionPresenter(DamageUseCase damageUseCase, HealUseCase healUseCase)
    {
        _damageUseCase = damageUseCase;
        _healUseCase = healUseCase;
    }

    public bool ApplyDamage(DamageData data)
    {
        Debug.Log($"[Heath Controller] Applying damage: {data.Amount} to target: {data.Target}");
        return _damageUseCase.ExecuteDamage(data.Target, data.Amount, data.Knockback);
    }

    public bool ApplyHeal(HealData data)
    {
        return _healUseCase.ExecuteHeal(data.Target, data.Amount);
    }
}
