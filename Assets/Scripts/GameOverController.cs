using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{


    [SerializeField]

    private GameObject _restartBtn;

    [SerializeField]

    private GameObject _goMenuBtn;

    [SerializeField]
    public  GameObject _gameOverCanvas;

    public void GoMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Initial_MenuScene");
    }

    public void Restart()
    {
        _gameOverCanvas.SetActive(false);
        
        Time.timeScale = 1f;

        SceneManager.LoadScene("GameplayScene");
    }
}
