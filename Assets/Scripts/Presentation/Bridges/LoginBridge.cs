using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Application.UseCases;
using Assets.Scripts.Domain.Interfaces;
using Assets.Scripts.Infrastructure.Repositories;
using Assets.Scripts.Presentation.Controllers;
using Assets.Scripts.Presentation.Views;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Presentation.Bridges
{
    public class LoginBridge : MonoBehaviour
    {
        [Inject] private LoginUseCase useCase;

        [SerializeField] private TMP_InputField inputUserName;
        [SerializeField] private TMP_InputField inputPassword;
        [SerializeField] private LoginRegisterUIController uiController;

        private LoginController loginController;

        void Start()
        {
            
            loginController = new LoginController(useCase, uiController);
        }

        public void OnLoginClick()
        {
            loginController.Login(inputUserName.text, inputPassword.text);
        }
    }
}
