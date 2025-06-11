using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{

    [SerializeField]

    private GameObject _pauseBtn;

    [SerializeField]

    private GameObject _pauseMenu;    

    public void Pause() {
        Time.timeScale = 0f;
        _pauseBtn.SetActive(false);
        _pauseMenu.SetActive(true);
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
