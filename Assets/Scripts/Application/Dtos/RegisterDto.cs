using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Domain.Dtos
{
    public class RegisterDto
    {

        public String UserName { get; set; }

        public String Password { get; set; }

        public String ConfirmPassword { get; set; }

        public List<String> Validate() {

            var errors = new List<string>();

            if (string.IsNullOrEmpty(UserName))
                errors.Add("El nombre de usuario es obligatorio.");

            if (string.IsNullOrEmpty(Password))
                errors.Add("La contraseña es obligatoria.");

            if (Password != ConfirmPassword)
                errors.Add("Las contraseñas no coinciden.");

            if (Password.Length < 6)
                errors.Add("La contraseña debe tener al menos 6 caracteres.");

            return errors;

        }

    }
}
