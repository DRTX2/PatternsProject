using UnityEngine;
using Zenject;

public class DamageView : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private Vector2 knockback = new Vector2(5f, 0);

    [Inject] private HealthInteractionPresenter _interactionController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out var target))
        {
            float direction = transform.localScale.x >= 0 ? 1f : -1f;
            knockback.x *= direction;
            var damageData = new DamageData
            {
                Target = target,
                Amount = damage,
                Knockback = knockback
            };
            Debug.Log($"[Damage View]Applying damage: {damageData.Amount} to target: {target}");
            bool wasDamaged = _interactionController.ApplyDamage(damageData);
        }
    }
}
