using Assets.Scripts.Application.Dtos;
using UnityEngine;
using Zenject;

/// <summary>
/// DiamondPickup representa un objeto recolectable que otorga score al jugador.
/// Utiliza inyección de dependencias con Zenject para acceder al ScorePresenter.
/// </summary>
public class DiamondPickup : MonoBehaviour
{
    [SerializeField] private int scoreValue = 10;

    [Inject] public ScorePresenter _presenter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IScoreCollectibleBehaviour>(out var target))
        {
            var data = new ScoreCollectData
            {
                Target = target,
                Amount = scoreValue
            };
            ScorePresenter sp = new ScorePresenter(new CollectScoreUseCase());
            sp.ApplyScore(data);
            //_presenter.ApplyScore(data);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        transform.eulerAngles += new Vector3(0, 180, 0) * Time.deltaTime;
    }

    private void Awake()
    {
        if (_presenter == null)
        {
            Debug.LogError("❌ ScorePresenter no fue inyectado en DiamondPickup.");
        }
        else
        {
            Debug.Log("✅ ScorePresenter inyectado correctamente en DiamondPickup.");
        }
    }
}
