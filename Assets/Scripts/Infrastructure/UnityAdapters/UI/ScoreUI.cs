using UnityEngine;
using TMPro;
using Zenject;

/// <summary>
/// ScoreUI es un componente visual que muestra el puntaje actual del jugador.
/// Inicializa con el valor real y luego escucha eventos del CharacterEventBus para actualizarse.
/// </summary>
public class ScoreUI : MonoBehaviour
{
    [Inject] private Player _player;
    [Inject] private CharacterEventBus _eventBus;

    [SerializeField] private TMP_Text scoreText;

    private void Start()
    {
        scoreText.text = $"Score: {_player.Score}";
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
