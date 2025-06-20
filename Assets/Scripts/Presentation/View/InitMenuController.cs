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
        if (user == null)
        {
            Debug.LogWarning("❌ No hay usuario en sesión.");
            return;
        }

        // Debug: mostrar todos los datos del usuario en consola
        Debug.Log($"✅ Usuario logueado:\n" +
                  $"Username: {user.UserName}\n" +
                  $"Nivel actual: {user.CurrentLevel}\n" +
                  $"Score: {user.Score}\n" +
                  $"Posición: ({user.PositionX}, {user.PositionY})\n" +
                  $"Enemigos eliminados: {user.EnemiesEliminated}\n" +
                  $"Vida: {user.Health}");

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
