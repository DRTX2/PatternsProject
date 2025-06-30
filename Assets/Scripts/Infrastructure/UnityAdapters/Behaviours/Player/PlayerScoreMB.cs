using UnityEngine;
using Zenject;

/// <summary>
/// Adaptador MonoBehaviour que conecta el sistema de score visual con el modelo Player.    
/// </summary>
public class PlayerScoreMB : MonoBehaviour, IScoreCollectibleBehaviour
{
    [Inject] private Player _player;
    [Inject] private CharacterEventBus _eventBus;

    public int Score => _player.Score;

    public void CollectScore(int amount)
    {
        _player.CollectScore(amount);

        _eventBus.ScoreCollected.Notify(new ScoreCollectedEvent
        {
            TotalScore = _player.Score,
            CollectedAmount = amount
        });
    }
}
