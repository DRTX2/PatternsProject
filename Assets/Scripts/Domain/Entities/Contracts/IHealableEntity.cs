/// <summary>
/// Contract for entities that can be healed.
/// Defines the properties and methods required for healing functionality.
/// </summary>
public interface IHealableEntity
{
    Health Health { get; }
    bool TryHeal(float amount);
    bool IsAlive => Health.Current > 0;
}