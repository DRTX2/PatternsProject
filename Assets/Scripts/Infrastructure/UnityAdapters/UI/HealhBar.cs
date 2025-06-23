using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Zenject;

/// <summary>
/// HealthBar is a UI component that displays the player's health.
/// </summary>
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text healthText;

    [Inject] private IDamageBehaviour playerDamageable;
    [Inject] private CharacterEventBus _eventBus;

    void Start()
    {
        slider.value = playerDamageable.CurrentHealth / playerDamageable.MaxHealth;
        healthText.text = $"HP {playerDamageable.CurrentHealth} / {playerDamageable.MaxHealth}";
    }

    private void OnEnable()
    {
        _eventBus.HealthChanged.Subscribe(UpdateHealthBar);
    }

    private void OnDisable()
    {
        _eventBus.HealthChanged.Unsubscribe(UpdateHealthBar);
    }

    private void UpdateHealthBar(HealthChangedEvent e)
    {
        if (!e.Character.CompareTag("Player")) return;

        slider.value = e.Current/ e.Max;
        healthText.text = $"HP {e.Current} / {e.Max}";
    }
}
