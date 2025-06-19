using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Domain.Dtos;
using Assets.Scripts.Domain.Entities;
using Assets.Scripts.Domain.Interfaces;

namespace Assets.Scripts.Application.UseCases
{
    internal class RegisterUseCase
    {

        private readonly IUserRepository _repository;

        public (UserData? data, List<string> errors) Execute(RegisterDto registerDto)
        {
            var errors = registerDto.Validate();

            if (errors.Count > 0)
                return (null, errors);

            var entity = new UserData
            {
                UserName = registerDto.UserName,
                Password = registerDto.Password
            };

            var result = _repository.Register(entity);

            return (result, new List<string>());
        }
    }
}
