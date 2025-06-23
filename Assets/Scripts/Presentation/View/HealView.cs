using UnityEngine;
using Zenject;

/// <summary>
/// HealView is responsible for handling the healing interaction in the game.
/// Abstracts the healing logic from the game objects and uses a presenter to apply healing.
/// Provides a visual representation of the healing item that rotates over time.
/// </summary>
/// 
public class HealView : MonoBehaviour 
{
    [SerializeField] private float healAmount = 20f;

    [Inject] private HealthPresenter _presenter;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IHealBehaviour>(out var target))
        {
            var data = new HealData
            {
                Target = target,
                Amount = healAmount
            };

            if (_presenter.ApplyHeal(data))
            {
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        transform.eulerAngles += new Vector3(0, 180, 0) * Time.deltaTime;
    }
}
