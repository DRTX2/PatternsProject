/// <summary>
/// IDamageBehaviour is an interface that defines the behavior for entities that can receive damage.
/// Any class implementing this interface must provide functionality to handle health, damage reception, and status checks.
/// Abstracts the damage logic from the entity itself, allowing for different damage handling strategies.
/// 
/// </summary>

public interface IDamageBehaviour
{
    float CurrentHealth { get; }
    float MaxHealth { get; }
    bool ReceiveDamage(float amount, UnityEngine.Vector2 knockback);
    bool _isAlive { get; }
}
