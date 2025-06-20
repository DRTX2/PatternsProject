using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Application.Session;
using Assets.Scripts.Application.UseCases;
using Assets.Scripts.Domain.Dtos;
using Assets.Scripts.Presentation.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.Collections.Specialized.BitVector32;

namespace Assets.Scripts.Presentation.Controllers
{
    public class LoginController
    {
        private readonly LoginUseCase _useCase;
        private readonly IPresenter _presenter;
        private readonly Session _session;
        public LoginController(LoginUseCase useCase, IPresenter presenter, Session session)
        {
            _useCase = useCase;
            _presenter = presenter;
            _session = session;
        }

        public void Login(string username, string password)
        {
            var dto = new LoginDto
            {
                UserName = username,
                Password = password
            };

            var (user, errors) = _useCase.Execute(dto);

            if (errors.Count > 0)
                _presenter.ShowErrors(errors);
            else
            {
                //_presenter.HideErrors();
                //UnityEngine.Debug.Log("Login exitoso: " + user.UserName);
                _session.Login(user);
                SceneManager.LoadScene(SceneNames.Get(SceneName.Initial_MenuScene));
            }
        }
    }


}
