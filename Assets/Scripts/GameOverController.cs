using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private GameObject _restartBtn;
    [SerializeField] private GameObject _goMenuBtn;
    [SerializeField] private GameObject _gameOverCanvas;

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
        //_gameOverCanvas.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameplayScene");
    }


}