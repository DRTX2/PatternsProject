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
    internal class RegisterController : MonoBehaviour
    {
        [SerializeField] private TMP_InputField usernameInput;
        [SerializeField] private TMP_InputField passwordInput;
        [SerializeField] private TMP_InputField confirmInput;
        [SerializeField] private LoginRegisterUIController uiController;

        private RegisterUseCase _useCase;

        public void Init(RegisterUseCase useCase)
        {
            _useCase = useCase;
        }

        public void OnRegisterClick()
        {
            var dto = new RegisterDto
            {
                UserName = usernameInput.text,
                Password = passwordInput.text,
                ConfirmPassword = confirmInput.text
            };

            var (user, errors) = _useCase.Execute(dto);
            if (errors.Count > 0)
            {
                uiController.ShowErrors(errors);
            }
            else
            {
                uiController.HideErrors();
                Debug.Log("Registro exitoso: " + user.UserName);
              
            }
        }
    }
}
