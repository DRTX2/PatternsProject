using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Zenject;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text healthText;
    [Inject] private IDamageable playerDamageable;

    void Start()
    {
        slider.value = playerDamageable.CurrentHealth / playerDamageable.MaxHealth;
        healthText.text = $"HP {playerDamageable.CurrentHealth} / {playerDamageable.MaxHealth}";
    }

    private void OnEnable()
    {
        CharacterEvents.OnHealthChanged.AddListener(UpdateHealthBar);
    }

    private void OnDisable()
    {
        CharacterEvents.OnHealthChanged.RemoveListener(UpdateHealthBar);
    }

    private void UpdateHealthBar(GameObject target, float current, float max)
    {
        if (!target.CompareTag("Player")) return;

        slider.value = current / max;
        healthText.text = $"HP {current} / {max}";
    }
}
