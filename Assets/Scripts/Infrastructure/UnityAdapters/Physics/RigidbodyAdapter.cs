using UnityEngine;

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
        Debug.Log($"RigidbodyAdapter: Applying knockback with x: {knockback.x}, y: {knockback.y} to Rigidbody2D: {_rb.gameObject.name}. Current Velocity: {_rb.linearVelocity}");
        _rb.linearVelocity = new Vector2(knockback.x, _rb.linearVelocity.y + knockback.y);
        Debug.Log($"RigidbodyAdapter: Applied knockback with x: {knockback.x}, y: {knockback.y}. New velocity: {_rb.linearVelocity}");
    }
}
