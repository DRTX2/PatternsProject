/// <summary>
/// Use case for executing healing actions.
/// This class encapsulates the logic for healing an entity.
/// </summary>

public class HealUseCase
{
    public bool ExecuteHeal(IHealBehaviour healer, float amount)
    {
        return healer.Heal(amount);
    }
}