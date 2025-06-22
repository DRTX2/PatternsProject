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
            if (healed) Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.eulerAngles += new Vector3(0, 180, 0) * Time.deltaTime;
    }
}
