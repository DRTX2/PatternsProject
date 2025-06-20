using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Application.Session;
using Assets.Scripts.Application.UseCases;
using Assets.Scripts.Presentation.Controllers;
using Assets.Scripts.Presentation.Views;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Presentation.Bridges
{
    public class RegisterBridge : MonoBehaviour
    {
        [Inject] private RegisterController controller;

        [SerializeField] private TMP_InputField usernameInput;
        [SerializeField] private TMP_InputField passwordInput;
        [SerializeField] private TMP_InputField confirmInput;

        public void OnRegisterClick()
        {
            controller.Register(usernameInput.text, passwordInput.text, confirmInput.text);
        }
    }
}
