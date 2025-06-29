using Assets.Scripts.Application.Session;
using Assets.Scripts.Application.UseCases;
using Assets.Scripts.Presentation.Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameOverController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject _restartBtn;
    [SerializeField] private GameObject _goMenuBtn;
    [SerializeField] private GameObject _gameOverCanvas;

    [SerializeField] private TMP_Text enemiesDefeatedText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text finalScoreText;

    [Inject] private RestartGameUseCase _useCase;
    [Inject] private Session _session;
    [Inject] private Player _player;

    private RestartGameController _restartController;

    private void Start()
    {
        // Validar dependencias
        if (_useCase == null || _session == null || _player == null)
        {
            Debug.LogError("Dependencias no inyectadas correctamente en GameOverController.");
            return;
        }

        _restartController = new RestartGameController(_useCase, _session);

        // Validar referencias de UI
        if (_gameOverCanvas == null)
        {
            _gameOverCanvas = GameObject.Find("Canvas_Game_Over");
            if (_gameOverCanvas == null)
            {
                Debug.LogError("Canvas_Game_Over no encontrado en la escena.");
            }
        }
    }

    public void ShowGameOver()
    {
        if (_gameOverCanvas == null) return;

        _gameOverCanvas.SetActive(true);
        Time.timeScale = 0f;

        UpdateGameOverUI();
    }

    public void Restart()
    {
        _restartController.Restart();
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
        Debug.Log($"[GameOver] Score: {_player.Score}, Enemies: {_player.EnemiesEliminated}");

        if (enemiesDefeatedText != null)
            enemiesDefeatedText.text = _player.EnemiesEliminated.ToString();

        if (scoreText != null)
            scoreText.text = _player.Score.ToString();

        if (finalScoreText != null)
        {
            int finalScore = _player.Score + (_player.EnemiesEliminated * 10);
            finalScoreText.text = finalScore.ToString();
            Debug.Log(finalScore);

        }
    }
}
