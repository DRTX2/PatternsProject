using Assets.Scripts.Application.Session;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class InitMenuController : MonoBehaviour
{
    [Inject] private Session _session;
    [SerializeField] private TMP_Text playTxt;
    [SerializeField] private GameObject changePasswordPanel;

    private void Start()
    {
        var user = _session.CurrentUser;

        if (user.CurrentLevel == null)
            playTxt.SetText("Nuevo Juego");
        else
            playTxt.SetText("Continuar");

        changePasswordPanel.SetActive(false);

    }



    public void HidePanelChangePassword()
    {
        changePasswordPanel.SetActive(false);
    }

    public void ShowPanelChangePassword()
    {
        changePasswordPanel.SetActive(true);
    }

    public void OnPlay()
    {
    SceneManager.LoadScene("GameplayScene");

    }

   


    public void OnGoLogin()
    {
        _session.Logout();
        SceneManager.LoadScene("Login_RegisterScene");
    }
}
