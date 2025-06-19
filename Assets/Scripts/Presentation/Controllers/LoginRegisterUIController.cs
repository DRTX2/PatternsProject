using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Presentation.Controllers
{
    public class LoginRegisterUIController : MonoBehaviour
    {
        [SerializeField] private GameObject registerPanel;
        [SerializeField] private GameObject loginPanel;
        [SerializeField] private GameObject errorPanel;
        [SerializeField] private TMP_Text errorText;
        [SerializeField] private Button linkToRegister;
        [SerializeField] private Button linkToLogin;

        private void Start()
        {
            ShowLogin();
            errorPanel.SetActive(false);
            linkToRegister.onClick.AddListener(ShowRegister);
            linkToLogin.onClick.AddListener(ShowLogin);
        }

        public void ShowLogin()
        {
            loginPanel.SetActive(true);
            registerPanel.SetActive(false);
            errorPanel.SetActive(false);
        }

        public void ShowRegister()
        {
            loginPanel.SetActive(false);
            registerPanel.SetActive(true);
            errorPanel.SetActive(false); 
        }

        public void ShowErrors(List<string> errors)
        {
            errorPanel.SetActive(true);
            errorText.text = string.Join("\n", errors);
        }

        public void HideErrors()
        {
            errorPanel.SetActive(false);
            errorText.text = "";
        }
    }
}
