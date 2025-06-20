using Assets.Scripts.Application.Session;
using Assets.Scripts.Application.UseCases;
using Assets.Scripts.Presentation.Controllers;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameOverController : MonoBehaviour
{


    [SerializeField] private GameObject _restartBtn;
    [SerializeField] private GameObject _goMenuBtn;
    [SerializeField] private GameObject _gameOverCanvas;


    [Inject] private RestartGameUseCase _useCase;
    [Inject] private Session _session;

    private RestartGameController _restartController;

    private void Start()
    {
        _restartController = new RestartGameController(_useCase, _session);
    }

    public void ShowGameOver()
    {
        if (_gameOverCanvas == null)
        {
            _gameOverCanvas = GameObject.Find("Canvas_Game_Over");
        }

        _gameOverCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GoMenu()
    {
        //_gameOverCanvas.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Initial_MenuScene");
    }

    public void Restart()
    {
        _restartController.Restart();
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameplayScene");
    }


}