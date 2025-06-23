using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Domain.Entities;
using Assets.Scripts.Domain.Interfaces;
using Assets.Scripts.Infrastructure.Data.sqlite;

namespace Assets.Scripts.Infrastructure.Repositories
{
    internal class UserRepositorySqlite : IUserRepository
    {
        private readonly UserModel _model;

        public UserRepositorySqlite()
        {
            _model = new UserModel();
        }

        public UserData LoadGame(int id)
        {
            return _model.LoadGame(id);
        }

        public UserData Login(UserData entity)
        {
            return _model.Login(entity);
        }

        public UserData Register(UserData entity)
        {
            return _model.Register(entity);
        }

        public void RestartGame(int id)
        {
            _model.RestartGame(id);
        }

        public bool SaveGame(UserData entity)
        {
            return _model.SaveGame(entity);
        }
    }
}
