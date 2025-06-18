using UnityEngine;

public class Attack_old : MonoBehaviour
{
    public float attackDamage = 10f;
    public Vector2 knockback = Vector2.zero;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 1) ¿El otro puede recibir daño?
        Damageable_old damageable = collision.GetComponent<Damageable_old>();
        if (damageable == null) return;

        // 2) ¿Este hitbox pertenece al jugador?
        PlayerController player = GetComponentInParent<PlayerController>();
        if (player != null)
        {
            // Solo permitir daño si el jugador está atacand
            if (!player.IsAttacking)
                return;
        }
        // Si no es del jugador, se asume (por ahora) que viene de un enemigo
        // y se deja pasar el daño.

        // 3) Aplicar daño y retroceso
        Vector2 deliveredKnockback = transform.localScale.x > 0
            ? knockback
            : new Vector2(-knockback.x, knockback.y);

        bool gotHit = damageable.Hit(attackDamage, deliveredKnockback);
        if (gotHit)
            Debug.Log($"{collision.name} hit for {attackDamage}");
    }
}
