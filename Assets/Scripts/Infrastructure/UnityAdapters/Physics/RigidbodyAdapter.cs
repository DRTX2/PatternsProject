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

    public void SetVelocity(Vector2D velocity)
    {
        _rb.linearVelocity = new Vector2(velocity.X, velocity.Y);
    }

    public Vector2D GetVelocity()
    {
        return new Vector2D(_rb.linearVelocity.x, _rb.linearVelocity.y);
    }

    public void ApplyKnockback(Vector2D knockback)
    {
        _rb.linearVelocity = new Vector2(knockback.X, _rb.linearVelocity.y + knockback.Y);
    }

    public void SetGravityScale(float scale)
    {
        _rb.gravityScale = scale;
    }
}
