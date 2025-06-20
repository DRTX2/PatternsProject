using Assets.Scripts.Application.Session;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class InitMenuController : MonoBehaviour
{
    [Inject] private Session _session;
    [SerializeField] private TMP_Text playTxt;

    private void Start()
    {
        var user = _session.CurrentUser;

        if (user.CurrentLevel == null)
            playTxt.SetText("Nuevo Juego");
        else
            playTxt.SetText("Continuar");
    }
    public void OnPlay()
    {
    SceneManager.LoadScene("GameplayScene");

    }



    public void OnGoLogin()
    {
//TODO
    }
}
