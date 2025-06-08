using UnityEngine;

public class Attack : MonoBehaviour
{
    public float attackDamage=10f;
    public Vector2 knockback = Vector2.zero;
    void OnTriggerEnter2D(Collider2D collision)
    {
        // see if it can be hit
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            // hit the target
            bool gotHit= damageable.Hit(attackDamage,knockback);
            
            if (gotHit)
                Debug.Log(collision.name+" hit for "+attackDamage);

        }

    }
}
