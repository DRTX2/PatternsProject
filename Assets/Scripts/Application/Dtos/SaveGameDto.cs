using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Domain.Dtos
{
    public class SaveGameDto
    {
        public string UserName { get; set; }

        public int  Id{ get; set; }
        public string CurrentLevel { get; set; }
        public int Score { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public int EnemiesEliminated { get; set; }
        public int Health { get; set; }

        public List<string> Validate()
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(UserName))
                errors.Add("El nombre de usuario es obligatorio.");

            if (Health < 0 || Health > 100)
                errors.Add("La salud debe estar entre 0 y 100.");

            return errors;
        }
    }
}
