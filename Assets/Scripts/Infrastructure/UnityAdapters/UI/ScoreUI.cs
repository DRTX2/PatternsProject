using UnityEngine;
using TMPro;
using Zenject;

/// <summary>
/// ScoreUI es un componente visual que muestra el puntaje actual del jugador.
/// Escucha los eventos del CharacterEventBus y actualiza el texto.
/// </summary>
public class ScoreUI : MonoBehaviour
{
    [Inject] private CharacterEventBus _eventBus;

    [SerializeField] private TMP_Text scoreText;

    private void Start()
    {
        // Inicializa en 0 por defecto (puedes modificar según el diseño)
        scoreText.text = "Score: 0";
    }

    private void OnEnable()
    {
        _eventBus.ScoreCollected.Subscribe(UpdateScoreUI);
    }

    private void OnDisable()
    {
        _eventBus.ScoreCollected.Unsubscribe(UpdateScoreUI);
    }

    private void UpdateScoreUI(ScoreCollectedEvent evt)
    {
        scoreText.text = $"Score: {evt.TotalScore}";
    }
}
