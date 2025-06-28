using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Application.Session;
using Assets.Scripts.Application.UseCases;
using Assets.Scripts.Domain.Dtos;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Zenject.SpaceFighter;
namespace Assets.Scripts.Presentation.Controllers
{
    public class SaveGameController
    {
        private readonly Player _player;

        private readonly SaveGameUseCase _useCase;
        private readonly Session _session;

        public SaveGameController(SaveGameUseCase useCase, Session session, Player player)
        {
            _useCase = useCase;
            _session = session;
            _player = player;
        }

        public void Save()
        {
            var user = _session.CurrentUser;

            if (user == null)
            {
                Debug.LogError("❌ No hay usuario en sesión para guardar.");
                return;
            }

            user.PositionX = _player.PositionX;
            user.PositionY = _player.PositionY;
            user.Score = _player.Score;
            user.Health = (int)_player.Health.Current;
            user.EnemiesEliminated = _player.EnemiesEliminated;
            user.CurrentLevel = SceneManager.GetActiveScene().name;

            var dto = new SaveGameDto
            {
                UserName = user.UserName,
                CurrentLevel = user.CurrentLevel,
                Score = user.Score,
                PositionX = user.PositionX,
                PositionY = user.PositionY,
                EnemiesEliminated = user.EnemiesEliminated,
                Health = user.Health,
                Id = user.Id
            };

            var (savedUser, errors) = _useCase.Execute(dto);

            if (errors.Count > 0)
            {
                Debug.LogError("❌ Error al guardar el juego: " + string.Join(", ", errors));
            }
            else
            {
                Debug.Log("✅ Juego guardado exitosamente.");
            }
        }
    }
}