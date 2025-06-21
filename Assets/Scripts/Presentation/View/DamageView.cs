using UnityEngine;
using Zenject;

public class DamageView : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private Vector2 knockback = new Vector2(5f, 0);

    [Inject] private HealthInteractionPresenter _interactionController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"DamageView detected collision with: {other.name}");
        if (other.TryGetComponent<IDamageable>(out var target))
        {
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
