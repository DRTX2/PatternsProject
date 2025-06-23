using UnityEngine;
/// <summary>
/// RigidbodyAdapter is an adapter class that wraps a Unity Rigidbody2D component.
/// Implements the IPhysicsAdapter interface to provide a consistent API for manipulating physics properties.
/// </summary>
public class RigidbodyAdapter : IPhysicsAdapter
{
    private readonly Rigidbody2D _rb;

    public RigidbodyAdapter(Rigidbody2D rb)
    {
        _rb = rb;
    }

    public void SetVelocity(Vector2 velocity)
    {
        _rb.linearVelocity = velocity;
    }

    public Vector2 GetVelocity()
    {
        return _rb.linearVelocity;
    }

    public void ApplyKnockback(Vector2 knockback)
    {
        _rb.linearVelocity = new Vector2(knockback.x, _rb.linearVelocity.y + knockback.y);
    }

    public void SetGravityScale(float scale)
    {
        _rb.gravityScale = scale;
    }
}
