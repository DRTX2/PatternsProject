using UnityEngine;

public class DamageUseCase
{
    public bool ExecuteDamage(IDamageable target, float damage, Vector2 knockback)
    {
        return target.ReceiveDamage(damage, knockback);
    }
}
