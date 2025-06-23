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
    public class SaveGameUseCase
    {
        private readonly IUserRepository _repository;

        public SaveGameUseCase(IUserRepository repository)
        {
            _repository = repository;
        }

        public (UserData? data, List<string> errors) Execute(SaveGameDto dto)
        {
            var errors = dto.Validate();

            if (errors.Count > 0)
                return (null, errors);

            var entity = new UserData
            {
                UserName = dto.UserName,
                CurrentLevel = dto.CurrentLevel,
                Score = dto.Score,
                PositionX = dto.PositionX,
                PositionY = dto.PositionY,
                EnemiesEliminated = dto.EnemiesEliminated,
                Health = dto.Health,
                Id = dto.Id
            }; 

            try
            {
                var success = _repository.SaveGame(entity);
                if (!success)
                    return (null, new List<string> { "Error al guardar la partida." });

                return (entity, new List<string>());
            }
            catch (Exception ex)
            {
                return (null, new List<string> { ex.Message });
            }
        }
    }
    }
