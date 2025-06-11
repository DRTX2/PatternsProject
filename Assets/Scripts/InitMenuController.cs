using UnityEngine;
using UnityEngine.SceneManagement;

public class InitMenuController : MonoBehaviour
{
    public void OnPlay()
    {
        SceneManager.LoadScene("GameplayScene");
    }

    public void OoLoad()
    {
        //TODO
    }

    public void OnExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
