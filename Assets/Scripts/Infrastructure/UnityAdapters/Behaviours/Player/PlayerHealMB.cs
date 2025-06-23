using UnityEngine;
using Zenject;

/// <summary>
/// PlayerHealMB is a MonoBehaviour that implements the IHealBehaviour interface.
/// </summary>
public class PlayerHealMB : MonoBehaviour, IHealBehaviour
{
    [Inject] private Player _player;
    [Inject] private CharacterEventBus _eventBus;

    public bool CanHeal => _player.Health.CanHeal;
    public float CurrentHealth => _player.Health.Current;
    public float MaxHealth => _player.Health.Max;

    public bool Heal(float amount)
    {
        if (_player.TryHeal(amount))
        {
            _eventBus.Healed.Notify(new HealEvent { Character = gameObject, Amount = amount });
            _eventBus.HealthChanged.Notify(new HealthChangedEvent { Character = gameObject, Current = CurrentHealth, Max = MaxHealth });
            return true;
        }
        return false;
    }
}
