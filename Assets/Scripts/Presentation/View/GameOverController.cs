using Assets.Scripts.Application.Session;
using Assets.Scripts.Application.UseCases;
using Assets.Scripts.Presentation.Controllers;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameOverController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject restartBtn;
    [SerializeField] private GameObject goMenuBtn;

    [SerializeField] private TMP_Text playerNameLbl;
    [SerializeField] private TMP_Text pointsEarnedLbl;
    [SerializeField] private TMP_Text healthRemainingLbl;

    [SerializeField] private CanvasGroup canvasGroup;

    [Inject] private RestartGameUseCase useCase;
    [Inject] private Session session;
    [Inject] private Player player;

    private RestartGameController restartController;

    private void Start()
    {
        restartController = new RestartGameController(useCase, session);

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.gameObject.SetActive(false);
        }
    }

    public void ShowGameOver()
    {
        if (canvasGroup == null)
        {
            Debug.LogWarning("CanvasGroup no asignado.");
            return;
        }

        UpdateGameOverUI();
        Time.timeScale = 0f;
        StartCoroutine(FadeInCanvas());
    }

    public void Restart()
    {
        restartController.Restart();
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameplayScene");
    }

    public void GoMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Initial_MenuScene");
    }

    private void UpdateGameOverUI()
    {
        string username = session.CurrentUser?.UserName ?? "Jugador";
        int health = (int)player.Health.Current;

        if (playerNameLbl != null)
            playerNameLbl.text = $"Jugador: {username}";

        if (pointsEarnedLbl != null)
            pointsEarnedLbl.text = $"Puntos Obtenidos: {player.Score}";

        if (healthRemainingLbl != null)
            healthRemainingLbl.text = $"Salud Restante: {health}";
    }

    private IEnumerator FadeInCanvas()
    {
        canvasGroup.gameObject.SetActive(true);
        canvasGroup.alpha = 0f;

        while (canvasGroup.alpha < 1f)
        {
            canvasGroup.alpha += Time.unscaledDeltaTime * 2f;
            yield return null;
        }

        canvasGroup.alpha = 1f;
    }
}
