using UnityEngine;
using Zenject;

public class DiamondView:MonoBehaviour
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