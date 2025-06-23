using UnityEngine;
/// <summary>
/// Data Transfer Object for damage information.
/// </summary>
public class DamageData
{
    public IDamageBehaviour Target;
    public float Amount;
    public Vector2 Knockback;
}
