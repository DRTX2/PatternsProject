using Assets.Scripts.Application.Session;
using Assets.Scripts.Application.UseCases;
using Assets.Scripts.Domain.Dtos;
using Assets.Scripts.Presentation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using static System.Collections.Specialized.BitVector32;

namespace Assets.Scripts.Presentation.Controllers
{
    public class LoginController
    {
        private readonly LoginUseCase _useCase;
        private readonly IPresenter _presenter;
        private readonly Session _session;
        private readonly LoadGameController _loader;
        private readonly DiContainer _container;

        [Inject]
        public LoginController(
            LoginUseCase useCase,
            IPresenter presenter,
            Session session,
            LoadGameController loader,   // ← inyectar el loader
            DiContainer container)
        {
            _useCase = useCase;
            _presenter = presenter;
            _session = session;
            _loader = loader;
            _container = container;
        }

        public void Login(string username, string password)
        {
            var dto = new LoginDto { UserName = username, Password = password };
            var (user, errors) = _useCase.Execute(dto);

            if (errors.Count > 0)
            {
                _presenter.ShowErrors(errors);
                return;
            }

            // 1) guardar usuario real en sesión
            _session.Login(user);

            // 2) crear Player (GameObject + entidad) a través del loader
            _loader.Load();                             // genera LoadedPlayer y lo añade a la escena
            var player = _loader.LoadedPlayer;

            // 3) vincular instancia al contenedor
            _container.Rebind<Player>()
                      .FromInstance(player)
                      .AsSingle();

            // 4) continuar normalmente
            SceneManager.LoadScene(SceneNames.Get(SceneName.Initial_MenuScene));
        }
    }
}