

using System;
using System.Collections.Generic;

    namespace Assets.Scripts.Domain.Entities
    {
        [Serializable]

        public class UserData
        {
            public int Id;
            public int Health;
            public string UserName;
            public string Password;
            public string CurrentLevel;
            public int Score;
            public float PositionX;
            public float PositionY;
            public int EnemiesEliminated;
        public List<string> OldPasswords = new List<string>();
        public UserData() { }

            public UserData(string userName, string password, string currentLevel, int score,
                            float positionX, float positionY, int enemiesEliminated, int health, int id)
            {
                UserName = userName;
                Password = password;
                CurrentLevel = currentLevel;
                Score = score;
                PositionX = positionX;
                PositionY = positionY;
                EnemiesEliminated = enemiesEliminated;
                Health = health;
                Id = id;
            }
        }
    }
