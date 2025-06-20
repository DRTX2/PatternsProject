using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;

namespace Assets.Scripts.Domain.Entities
{
    public class UserData
    {
        public int Health { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CurrentLevel { get; set; }
        public int Score { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public int EnemiesEliminated { get; set; }

        public UserData(string userName, string password, string currentLevel, int score, float positionX,float positionY, int enemiesEliminated, int health)
        {
            UserName = userName;
            Password = password;
            CurrentLevel = currentLevel;
            Score = score;
            PositionX = positionX;
            PositionY= positionY;
            EnemiesEliminated = enemiesEliminated;
            Health = health;
        }

        public UserData() { }
    }

}
