using Assets.Scripts.Presentation.Controllers;
using UnityEngine;
using Zenject;

public class FinishZoneTrigger : MonoBehaviour
{
    [SerializeField] private GameObject finishPanel;
    [Inject] private SaveGameController _saveGameController;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered || other.CompareTag("Player") || _saveGameController == null) return;

        _saveGameController.Save();

        if (finishPanel != null)
        {

            finishPanel.SetActive(true);

            // Llama explícitamente a ShowGameOver()
            GameOverController controller = finishPanel.GetComponent<GameOverController>();
            if (controller != null)
            {
                controller.ShowGameOver();
            }
            else
            {
                Debug.LogWarning("GameOverController no encontrado en el finishPanel.");
            }
        }

    }
}