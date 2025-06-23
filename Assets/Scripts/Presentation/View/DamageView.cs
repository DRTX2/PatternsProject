using UnityEngine;
using Zenject;

/// <summary>
/// DamageView is responsible for handling the damage interaction in the game.
/// Abstracts the damage logic from the game objects and uses a presenter to apply damage.
/// </summary>
public class DamageView : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private Vector2 knockback = new Vector2(5f, 0);

    [Inject] private DamagePresenter _presenter;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageBehaviour>(out var target))
        {
            float direction = transform.localScale.x >= 0 ? 1f : -1f;
            knockback.x *= direction;

            var data = new DamageData
            {
                Target = target,
                Amount = damage,
                Knockback = knockback
            };
            //Debug.Log($"Applying damage: {data.Amount} with knockback: {data.Knockback} to target: {target}");
            _presenter.ApplyDamage(data);
        }
    }
}
