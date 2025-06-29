using Assets.Scripts.Application.Session;
using Assets.Scripts.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Assets.Scripts.Presentation.Controllers
{
    public class RestartGameController
    {
        private readonly RestartGameUseCase _restartGameUseCase;
        private readonly Session _session;

        public RestartGameController(RestartGameUseCase restartGameUseCase, Session session)
        {
            _restartGameUseCase = restartGameUseCase;
            _session = session;
        }

        public void Restart()
        {
            var user = _session.CurrentUser;
            if (user != null)
            {
                Debug.Log($"🔁 Reiniciando datos para el usuario ID: {user.Id}");
                _restartGameUseCase.Execute(user.Id);
                // ⚠️ Esto reinicia la escena actual
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                Debug.LogWarning("⚠️ No se encontró un usuario en sesión al reiniciar.");
            }
        }
    }
}
