using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Application.Session;
using Assets.Scripts.Infrastructure.Repositories;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Presentation.Controllers
{
    public class ChangePasswordController : MonoBehaviour
    {
        [Inject] private Session _session;

        public TMP_InputField inputPassword;


        public GameObject MessaggesPanel;
        public GameObject ChangePasswordPanel;
        public TMP_Text messaggesText;    

        public void OnChangePasswordClick()
        {
            string newPassword = inputPassword.text;

            try
            {
                var repo = UserRepositoryJson.GetInstance();
                repo.ChangePassword(_session.CurrentUser.UserName, newPassword);

                _session.CurrentUser.Password = newPassword;
                HideChangePasswordPanel();
                MessaggesPanel.SetActive(true);
                messaggesText.SetText("Contraseña cambiada correctamente.");
            }
            catch (System.Exception ex)
            {              
                messaggesText.SetText(ex.Message);
                MessaggesPanel.SetActive(true);
            }
        }

        public void HideChangePasswordPanel() {
            ChangePasswordPanel.SetActive(false);
        }

        public void HideMessagesPanel() {
            MessaggesPanel.SetActive(false);
        }
    }

}
