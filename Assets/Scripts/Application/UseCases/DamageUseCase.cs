using UnityEngine;

/// <summary>
/// Use case for applying damage to a target.
/// This class encapsulates the logic for dealing damage,
/// </summary>

public class DamageUseCase
{
    public bool ExecuteDamage(IDamageBehaviour target, float amount, Vector2 knockback)
    {
        return target.ReceiveDamage(amount, knockback);
    }
}
