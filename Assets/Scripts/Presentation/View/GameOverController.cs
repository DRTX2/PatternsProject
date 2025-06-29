using Assets.Scripts.Application.Session;
using Assets.Scripts.Application.UseCases;
using Assets.Scripts.Presentation.Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameOverController : MonoBehaviour
{
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
    private float _startTime;

    private void Start()
    {
        _restartController = new RestartGameController(_useCase, _session);
        _startTime = Time.time;

    }

    public void ShowGameOver()
    {
        if (_gameOverCanvas == null)
        {
            _gameOverCanvas = GameObject.Find("Canvas_Game_Over");
        }

        _gameOverCanvas.SetActive(true);
        Time.timeScale = 0f;

        // ✅ Actualizar textos con los datos del jugador
        enemiesDefeatedText.text = _player.EnemiesEliminated.ToString();
        scoreText.text = _player.Score.ToString();

        int finalScore = _player.Score + (_player.EnemiesEliminated * 10); // Ejemplo de fórmula
        finalScoreText.text = finalScore.ToString();
    }

    public void GoMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Initial_MenuScene");
    }

    public void Restart()
    {
        _restartController.Restart();
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameplayScene");
    }
    //public void ShowGameOver()
    //{
    //    float elapsed = Time.time - _startTime;
    //    timeText.text = elapsed.ToString("F2") + "s";
    //}
}
