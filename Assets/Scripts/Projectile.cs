using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage= 10;
    public Vector2 moveSpeed = new Vector2(3f,0);
    public Vector2 knockback = new Vector2(0, 0);

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // if you want the projectile to be affected by gravitiy by default, make it dynamic mode rigidbody
        rb.linearVelocity = new Vector2(moveSpeed.x*transform.localScale.x, moveSpeed.y);   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable_old damageable = collision.GetComponent<Damageable_old>();
        if (damageable == null) return;

        Vector2 deliveredKnockback = transform.localScale.x > 0
            ? knockback
            : new Vector2(-knockback.x, knockback.y);

        bool gotHit = damageable.Hit(damage, deliveredKnockback);
        if (gotHit)
        {
            Debug.Log($"{collision.name} hit for {damage}");
            Destroy(gameObject); // Destroy the projectile after hitting
        }
    }
}
