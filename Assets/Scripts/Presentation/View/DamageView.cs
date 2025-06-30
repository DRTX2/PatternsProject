using UnityEngine;
using Zenject;

/// <summary>
/// DamageView is responsible for handling the damage interaction in the game.
/// Abstracts the damage logic from the game objects and uses a presenter to apply damage.
/// </summary>
public class DamageView : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private Vector2D knockback = new Vector2D(5f, 0);

    [Inject] private DamagePresenter _presenter;

    private void Awake()
    {
        if (_presenter == null)
        {
            Debug.LogWarning("⚠️ Presenter no fue inyectado, creándolo manualmente (solo para test)");
            _presenter = new DamagePresenter(new DamageUseCase());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageBehaviour>(out var target))
        {
            float direction = transform.localScale.x >= 0 ? 1f : -1f;
            knockback.X *= direction;

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
