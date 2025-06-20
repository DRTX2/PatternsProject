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
    public class RegisterController
    {
        private readonly RegisterUseCase _useCase;
        private readonly IPresenter _presenter;
        private readonly Session _session;
        public RegisterController(RegisterUseCase useCase, IPresenter presenter, Session session)
        {
            _useCase = useCase;
            _presenter = presenter;
            _session = session;
        }

        public void Register(string username, string password, string confirmPassword)
        {
            var dto = new RegisterDto
            {
                UserName = username,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            var (user, errors) = _useCase.Execute(dto);
            if (errors.Count > 0)
            {
                _presenter.ShowErrors(errors);
            }
            else
            {
               _session.Login(user);
                SceneManager.LoadScene(SceneNames.Get(SceneName.Initial_MenuScene));
            }
        }
    }
}
