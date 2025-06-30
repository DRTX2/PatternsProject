using Assets.Scripts.Application.Session;
using Assets.Scripts.Application.UseCases;
using Assets.Scripts.Presentation.Controllers;
using UnityEngine;
using Zenject;

public class FinishZoneTrigger : MonoBehaviour
{
    [Header("Panel de fin de juego (con GameOverController)")]
    [SerializeField] private GameObject finishPanel;

    [Inject] private SaveGameController _saveGameController;
    [Inject] private Session _session;
    [Inject] private Player _player;
    [Inject] private SaveGameUseCase _useCase;

    // Usamos Construct para asegurarnos de que Zenject inyecta antes de Awake() o Start()
    [Inject]
    public void Construct(
        SaveGameController saveGameController,
        Session session,
        Player player,
        SaveGameUseCase useCase)
    {        _saveGameController = saveGameController;
        _session = session;
        _player = player;
        _useCase = useCase;
    }

    private void Start()
    {
        // Verificar que finishPanel y GameOverController estén asignados
        if (finishPanel == null)
        {
            Debug.LogWarning("⚠ FinishZoneTrigger: el campo 'finishPanel' no está asignado en el Inspector.");
        }
        else if (finishPanel.GetComponent<EndGameController>() == null)
        {
            Debug.LogWarning("⚠ FinishZoneTrigger: el GameObject asignado en 'finishPanel' NO contiene un GameOverController.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        // Guardar sólo si tenemos el controlador correctamente inyectado
        if (_saveGameController == null)
        {
            Debug.LogError("❌ FinishZoneTrigger: SaveGameController es null. Verifica tu installer.");
            return;
        }

        // Aquí llama al Save
        _saveGameController.Save();

        // Mostrar panel de Game Over
        if (finishPanel != null)
        {
            finishPanel.SetActive(true);

            var controller = finishPanel.GetComponent<EndGameController>();
            if (controller != null)
            {
                //Debug.Log("✅ GameOverController encontrado. Mostrando pantalla final.");
                controller.ShowGameOver();
            }
            // Los mensajes de warning ya los mostramos en Start()
        }
    }
}