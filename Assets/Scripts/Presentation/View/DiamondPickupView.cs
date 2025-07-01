using UnityEngine;
using Zenject;
using Assets.Scripts.Application.Dtos;

/// <summary>
/// DiamondPickupView se encarga de la interacción de recogida de diamantes.
/// Abstracta la lógica a través de un Presenter inyectado.
/// </summary>
public class DiamondPickupView : MonoBehaviour
{
    [SerializeField] private int scoreValue = 10;

    [Inject] private ScorePresenter _presenter;  // Inyectamos el presenter

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IScoreCollectibleBehaviour>(out var target))
        {
            var data = new ScoreCollectData
            {
                Target = target,
                Amount = scoreValue
            };

            // Aplica la puntuación; el presenter disparará el evento
            _presenter.ApplyScore(data);

            // Destruye el objeto sólo si el presenter lo permitió
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Rotación visual
        transform.eulerAngles += new Vector3(0, 180, 0) * Time.deltaTime;
    }
}
