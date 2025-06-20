using Assets.Scripts.Application.Session;
using Assets.Scripts.Application.UseCases;
using Assets.Scripts.Presentation.Controllers;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class PauseMenuController : MonoBehaviour
{

    [Inject] private SaveGameUseCase _useCase;
    [Inject] private Session _session;
    [Inject] private Player _player;
    private SaveGameController _controller;

    private void Start()
    {
        _controller = new SaveGameController(_useCase, _session, _player);
    }

    [SerializeField]

    private GameObject _pauseBtn;

    [SerializeField]

    private GameObject _pauseMenu;    

    public void Pause() {
        Time.timeScale = 0f;
        _pauseBtn.SetActive(false);
        _pauseMenu.SetActive(true);
    }

    public void Save()
    {
        _controller.Save();
       // Time.timeScale = 1f;
        //_pauseBtn.SetActive(true);
        //_pauseMenu.SetActive(false);
    }
    public void Resume() {
        Time.timeScale = 1f;
        _pauseBtn.SetActive(true);
        _pauseMenu.SetActive(false);
    }

    public void GoMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Initial_MenuScene");
    }

    public void Restart() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameplayScene");
    }

}
