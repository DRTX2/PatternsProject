using UnityEngine;

public class DamageUseCase
{
    public bool ExecuteDamage(IDamageable target, float damage, Vector2 knockback)
    {
        Debug.Log($"DamageUseCase: Executing damage on target with CurrentHealth: {target.CurrentHealth}, MaxHealth: {target.MaxHealth}, Damage: {damage}, Knockback: {knockback}");
        return target.ReceiveDamage(damage, knockback);
    }
}
