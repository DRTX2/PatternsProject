using UnityEngine;
using TMPro;
using Zenject;

/// <summary>
/// ScoreUI es un componente visual que muestra el puntaje actual del jugador.
/// Inicializa con el valor real y luego escucha eventos del CharacterEventBus para actualizarse.
/// </summary>
public class ScoreUI : MonoBehaviour
{
    [Inject] private Player _player;                   // Inyectamos el modelo de dominio
    [Inject] private CharacterEventBus _eventBus;      // El bus de eventos global

    [SerializeField] private TMP_Text scoreText;       // Referencia al TextMeshPro

    private void Start()
    {
        // 1️⃣ Mostrar el score inicial real
        scoreText.text = $"Score: {_player.Score}";
    }

    private void OnEnable()
    {
        // 2️⃣ Suscribirse a futuras actualizaciones
        _eventBus.ScoreCollected.Subscribe(UpdateScoreUI);
    }

    private void OnDisable()
    {
        _eventBus.ScoreCollected.Unsubscribe(UpdateScoreUI);
    }

    private void UpdateScoreUI(ScoreCollectedEvent evt)
    {
        // 3️⃣ Actualizar con el nuevo total recibido
        scoreText.text = $"Score: {evt.TotalScore}";
    }
}
