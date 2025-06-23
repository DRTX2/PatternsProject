/// <summary>
/// IHealBehaviour is an interface that defines the behavior for entities that can be healed.
/// Any class implementing this interface must provide functionality to handle health, healing, and status checks.
/// Abstracts the healing logic from the entity itself, allowing for different healing strategies.
/// </summary>

public interface IHealBehaviour
{
    float CurrentHealth { get; }
    float MaxHealth { get; }
    bool Heal(float amount);
    bool CanHeal { get; }
}
