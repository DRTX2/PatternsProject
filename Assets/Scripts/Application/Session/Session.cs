using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Domain.Entities;


namespace Assets.Scripts.Application.Session
{

    public class Session
    {
        public UserData? CurrentUser { get; private set; }

        public void Login(UserData user)

        {
           
            CurrentUser = user;
        }

        public void Logout()
        {
            CurrentUser = null;
        }

        public bool IsLoggedIn => CurrentUser != null;
    }

}