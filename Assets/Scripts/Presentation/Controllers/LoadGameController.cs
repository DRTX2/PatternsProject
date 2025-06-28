using Assets.Scripts.Application.Session;
using Assets.Scripts.Infrastructure.Data.sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace Assets.Scripts.Presentation.Controllers
{
    public class LoadGameController
    {
        private readonly Session _session;
        private readonly PlayerFactory _factory;

        public Player LoadedPlayer { get; private set; }

        [Inject]
        public LoadGameController(Session session, PlayerFactory factory)
        {
            _session = session;
            _factory = factory;
        }

        public void Load()
        {
            var user = _session.CurrentUser;

            if (user == null)
            {
                UnityEngine.Debug.LogError("❌ No hay usuario en sesión.");
                return;
            }

            LoadedPlayer = _factory.CreateFromUser(user); // ✅ convertir UserData en Player
            UnityEngine.Debug.Log("✅ Player cargado correctamente desde sesión.");
        }
    }
}
