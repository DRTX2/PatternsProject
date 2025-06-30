using Assets.Scripts.Application.Session;
using Assets.Scripts.Application.UseCases;
using Assets.Scripts.Presentation.Controllers;
using UnityEngine;
using Zenject;

public class FinishZoneTrigger : MonoBehaviour
{
    [SerializeField] private GameObject finishPanel;

    [Inject] private SaveGameController _saveGameController;

    [Inject] private Session _session;
    [Inject] private Player _player;
    [Inject] private SaveGameUseCase _useCase;

    private void Awake()
    {
        _saveGameController = new SaveGameController(_useCase, _session, _player);
        Debug.Log("[Trigger] SaveGameController instanciado manualmente.");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"[Trigger] Algo entró: {other.name}");

        if (_saveGameController == null)
        {
            Debug.LogError("❌ SaveGameController sigue siendo null. Verifica inyección.");
            return;
        }

        Debug.Log("[Trigger] ¡Jugador detectado!");
        //_saveGameController.Save();
        Debug.Log("bffbfbfbf[Trigger] ¡Jugador detectado!");

        if (finishPanel != null)
        {
            finishPanel.SetActive(true);
            var controller = finishPanel.GetComponent<GameOverController>();
            if (controller != null)
            {
                Debug.Log("✅ GameOverController encontrado. Mostrando pantalla final.");
                controller.ShowGameOver();
            }
            else
            {
                Debug.LogWarning("⚠️ GameOverController no encontrado en el panel.");
            }
        }
        else
        {
            Debug.LogWarning("⚠️ Finish panel no asignado.");
        }
    }
}