using UnityEngine;
using Zenject;

public class HealView : MonoBehaviour
{
    [Inject] private HealthInteractionPresenter interactionController;
    [SerializeField] public float healAmount = 20f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IHealable>(out var healable))
        {
            var healData = new HealData
            {
                Target = healable,
                Amount = healAmount
            };

            bool healed = interactionController.ApplyHeal(healData);
            if (healed)
            {
                Destroy(gameObject);
                CharacterEvents.OnHealed?.Invoke((healable as MonoBehaviour).gameObject, healAmount);
                CharacterEvents.OnHealthChanged?.Invoke((healable as MonoBehaviour).gameObject, healable.CurrentHealth, healable.MaxHealth);
            }
        }
    }

    private void Update()
    {
        transform.eulerAngles += new Vector3(0, 180, 0) * Time.deltaTime;
    }
}
