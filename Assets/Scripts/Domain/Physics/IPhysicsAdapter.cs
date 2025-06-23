using UnityEngine;

/// <summary>
/// IPhysicsAdapter is an interface that defines the behavior for physics-related operations on entities.
/// Used to abstract physics interactions such as velocity, knockback, and gravity scaling.
/// </summary>
public interface IPhysicsAdapter
{
    void SetVelocity(Vector2 velocity);
    Vector2 GetVelocity();
    void ApplyKnockback(Vector2 knockback);
    void SetGravityScale(float scale);
}
