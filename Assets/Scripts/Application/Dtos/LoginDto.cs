    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace Assets.Scripts.Domain.Dtos
    {
        internal class LoginDto
        {

            public String UserName { get; set; }

            public String Password { get; set; }



            public List<String> Validate()
            {

                var errors = new List<string>();
                if (string.IsNullOrEmpty(UserName))
                    errors.Add("Username is required.");
                if (string.IsNullOrEmpty(Password))
                    errors.Add("Password is required.");
                if (Password.Length < 6)
                    errors.Add("Password must be at least 6 characters long.");
                return errors;

            }
        }
    }
