using UnityEngine;
using Assets.Scripts.Domain.Entities;
using Assets.Scripts.Domain.Dtos;

public class PlayerFactory
{
    private readonly int _maxHealth;

    /// <summary>
    /// Inyectado por Zenject desde GlobalInstaller.
    /// </summary>
    public PlayerFactory(int maxHealth = 100)
    {
        if (maxHealth <= 0)
            throw new System.ArgumentOutOfRangeException(nameof(maxHealth), "La vida máxima debe ser mayor a cero.");

        _maxHealth = maxHealth;
    }

    /// <summary>
    /// Crea un jugador nuevo (vida llena, sin progreso).
    /// </summary>
    public Player CreateNew()
    {
        return new Player(
            maxHealth: _maxHealth,
            currentHealth: _maxHealth,
            positionX: 0f,
            positionY: 0f,
            enemiesEliminated: 0,
            score: 0
        );
    }

    /// <summary>
    /// Crea un jugador con datos específicos (cargados desde sesión).
    /// </summary>
    public Player Create(float positionX, float positionY, int score, int health, int enemiesEliminated)
    {
        float clampedHealth = Mathf.Clamp(health, 0, _maxHealth);

        return new Player(
            maxHealth: _maxHealth,
            currentHealth: clampedHealth,
            positionX: positionX,
            positionY: positionY,
            enemiesEliminated: enemiesEliminated,
            score: score
        );
    }
    public Player CreateFromUser(UserData userData)
    {
        float clampedHealth = Mathf.Clamp(userData.Health, 0, _maxHealth);

        return Create(
            positionX: userData.PositionX,
            positionY: userData.PositionY,
            score: userData.Score,
            health: (int) clampedHealth, // cast implícito de int → float
            enemiesEliminated: userData.EnemiesEliminated
        );
    }
}
