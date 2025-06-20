using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Domain.Interfaces;

namespace Assets.Scripts.Application.UseCases
{
    public class RestartGameUseCase
    {
        private readonly IUserRepository _repository;

        public RestartGameUseCase(IUserRepository repository)
        {
            _repository = repository;
        }

        public void Execute(int userId)
        {
            _repository.RestartGame(userId);
        }
    }
}
