using UnityEngine;

public interface IPhysicsAdapter
{
    void SetVelocity(Vector2 velocity);
    Vector2 GetVelocity();
    void ApplyKnockback(Vector2 knockback);
    void SetGravityScale(float scale);
}
