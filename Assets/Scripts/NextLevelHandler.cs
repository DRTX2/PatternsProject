using UnityEngine;
using Zenject;
using Assets.Scripts.Presentation.Controllers;

public class NextLevelHandler : MonoBehaviour
{
    [SerializeField] private Transform teleportTarget; // punto al que se moverá el jugador

    [Inject] private SaveGameController _saveGameController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (_saveGameController != null)
        {
            _saveGameController.Save();
            Debug.Log("💾 Juego guardado antes de cambiar de zona.");
        }

        // Mover al jugador a la posición de destino
        collision.transform.position = teleportTarget.position;
        Debug.Log("🚪 Jugador teletransportado a nueva zona.");
    }
}
