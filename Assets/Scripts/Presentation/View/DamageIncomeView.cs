using UnityEngine;
using Zenject;

public class DamageIncomeView : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private Vector2 knockback = new Vector2(5f, 0);

    [Inject] private HealthInteractionController _interactionController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out var target))
        {
            var damageData = new DamageData
            {
                Target = target,
                Amount = damage,
                Knockback = knockback
            };

            bool wasDamaged = _interactionController.ApplyDamage(damageData);
            if (wasDamaged)
            {
                CharacterEvents.OnDamageReceived?.Invoke((target as MonoBehaviour).gameObject, damage);
                CharacterEvents.OnHealthChanged?.Invoke((target as MonoBehaviour).gameObject, target.CurrentHealth, target.MaxHealth);
            }
        }
    }
}
