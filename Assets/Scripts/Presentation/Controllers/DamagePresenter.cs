using UnityEngine;

/// <summary>
/// DamagePresenter is responsible for handling the presentation logic related to damage application.
/// </summary>

public class DamagePresenter
{
    private readonly DamageUseCase _damageUseCase;

    public DamagePresenter(DamageUseCase damageUseCase)
    {
        _damageUseCase = damageUseCase;
    }

    public bool ApplyDamage(DamageData data)
    {
        return _damageUseCase.ExecuteDamage(data.Target, data.Amount, data.Knockback);
    }
}
