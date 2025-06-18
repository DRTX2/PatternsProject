public class HealUseCase
{
    public bool ExecuteHeal(IHealable target, float amount)
    {
        return target.Heal(amount);
    }
}