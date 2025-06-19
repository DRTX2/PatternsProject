using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Domain.Entities;

namespace Assets.Scripts.Application.Session
{
    public static class Session
    {
        public static UserData? CurrentUser { get; private set; }

        public static void Login(UserData user)
        {
            CurrentUser = user;
        }

        public static void Logout()
        {
            CurrentUser = null;
        }

        public static bool IsLoggedIn => CurrentUser != null;
    }
}
