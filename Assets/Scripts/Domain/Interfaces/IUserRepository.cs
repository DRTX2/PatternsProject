using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Domain.Entities;

namespace Assets.Scripts.Domain.Interfaces
{
    public interface IUserRepository
    {

        UserData Register(UserData entity);

        UserData Login(UserData entity);

        UserData LoadGame(int id);

       bool SaveGame(UserData entity);

        void RestartGame(int id);
    


        /*
        register

login

save

load

reset

        */
    }
}
