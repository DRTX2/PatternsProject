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

namespace Assets.Scripts.Presentation.Controllers
{
    public class RegisterController
    {
        private readonly RegisterUseCase _useCase;
        private readonly IPresenter _presenter;

        public RegisterController(RegisterUseCase useCase, IPresenter presenter)
        {
            _useCase = useCase;
            _presenter = presenter;
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
                Session.Login(user);
                SceneManager.LoadScene(SceneNames.Get(SceneName.Initial_MenuScene));
            }
        }
    }
}
