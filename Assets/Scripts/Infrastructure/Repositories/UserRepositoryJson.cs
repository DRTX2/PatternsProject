using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Config;
using Assets.Scripts.Domain.Entities;
using Assets.Scripts.Domain.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Repositories
{
    public class UserRepositoryJson : IUserRepository
    {
        private static UserRepositoryJson _instance;
        private static readonly object _lock = new();
        private readonly string filePath;

        private UserRepositoryJson()
        {
            filePath = Path.Combine(UnityEngine.Application.persistentDataPath, "users.json");
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, JsonUtilityWrapper.ToJsonList(new List<UserData>()));
            }
        }
        public static UserRepositoryJson GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    _instance ??= new UserRepositoryJson();
                }
            }

            return _instance;
        }

        public UserData Register(UserData entity)
        {
            var users = LoadAllUsers();

            if (users.Any(u => u.UserName == entity.UserName))
                throw new Exception("Ya existe ese usuario.");
            entity.Health = 100;
            entity.Id = GenerateNewId(users);
            entity.Password = Bcrypt.Encrypt(entity.Password);
            users.Add(entity);
            SaveAllUsers(users);
            Debug.Log($"Usuario registrado: ID={entity.Id}, Username={entity.UserName}, Score={entity.Score}, Health={entity.Health}");

            return entity;
        }

        public UserData Login(UserData entity)
        {
            var users = LoadAllUsers();
            var user = users.FirstOrDefault(u => u.UserName == entity.UserName);

            if (user == null)
                throw new Exception("Usuario no encontrado.");

            if (!Bcrypt.Compare(entity.Password, user.Password))
                throw new Exception("Contraseña incorrecta.");

            return user;
        }

        public UserData LoadGame(int id)
        {
            var user = LoadAllUsers().FirstOrDefault(u => u.Id == id);
            if (user == null)
                throw new Exception("Partida no encontrada.");
            return user;
        }

        public bool SaveGame(UserData entity)
        {
            var users = LoadAllUsers();
            var index = users.FindIndex(u => u.Id == entity.Id);
            if (index == -1) return false;

            users[index] = entity;
            SaveAllUsers(users);
            return true;
        }

        public void RestartGame(int id)
        {
            var users = LoadAllUsers();
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null) return;

            user.CurrentLevel = null;
            user.Score = 0;
            user.PositionX = 0;
            user.PositionY = 0;
            user.EnemiesEliminated = 0;
            user.Health = 100;

            SaveAllUsers(users);
        }

        private List<UserData> LoadAllUsers()
        {
            if (!File.Exists(filePath))
                return new List<UserData>();

            var json = File.ReadAllText(filePath);
            return JsonUtilityWrapper.FromJsonList<UserData>(json);
        }

        private void SaveAllUsers(List<UserData> users)
        {
            var json = JsonUtilityWrapper.ToJsonList(users);
            File.WriteAllText(filePath, json);
        }

        private int GenerateNewId(List<UserData> users)
        {
            return users.Count == 0 ? 1 : users.Max(u => u.Id) + 1;
        }

        public void ChangePassword(string username, string newPassword)
        {
            var users = LoadAllUsers();
            var user = users.FirstOrDefault(u => u.UserName == username);

            
            if (Bcrypt.Compare(newPassword, user.Password) ||
                user.OldPasswords.Any(old => Bcrypt.Compare(newPassword, old)))
            {
                throw new Exception("Contraseña ya Usada.");
            }

       
            user.OldPasswords ??= new List<string>();
            user.OldPasswords.Add(user.Password);

            user.Password = Bcrypt.Encrypt(newPassword);

            SaveAllUsers(users);
        }

    }
}
