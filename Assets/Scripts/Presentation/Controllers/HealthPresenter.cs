using UnityEngine;

/// <summary>
/// HealthPresenter is responsible for managing the health-related actions in the game.
/// </summary>
public class HealthPresenter
{
    private readonly HealUseCase _healUseCase;

    public HealthPresenter(HealUseCase healUseCase)
    {
        _healUseCase = healUseCase;
    }

    public bool ApplyHeal(HealData data)
    {
        return _healUseCase.ExecuteHeal(data.Target, data.Amount);
    }
}
