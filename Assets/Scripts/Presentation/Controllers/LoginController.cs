using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Application.UseCases;
using Assets.Scripts.Domain.Dtos;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Presentation.Controllers
{
    internal class LoginController
    {
        [SerializeField] private TMP_InputField inputUserName;
        [SerializeField] private TMP_InputField inputPassword;
        [SerializeField] private LoginRegisterUIController uiController;

        private LoginUseCase _useCase;

        public void Init(LoginUseCase useCase)
        {
            _useCase = useCase;
        }

        public void OnLoginClick()
        {
            var dto = new LoginDto
            {
                UserName = inputUserName.text,
                Password = inputPassword.text
            };

            var (user, errors) = _useCase.Execute(dto);
            if (errors.Count > 0)
            {
                uiController.ShowErrors(errors);
            }
            else
            {
                uiController.HideErrors();
                Debug.Log("Login exitoso: " + user.UserName);
              
            }
        }
    }
}
