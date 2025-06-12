using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public TMP_Text healthBarText;

    Damageable playerDamageable;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("No player found in the scene. Make sure it has tag 'Player'");
            return; //  corta la ejecución para evitar el null
        }

        playerDamageable = player.GetComponent<Damageable>();

        if (playerDamageable == null)
        {
            Debug.LogError("Player found, but no Damageable component attached.");
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthSlider.value = CalculateSliderPercentage(playerDamageable.Health, playerDamageable.MaxHealth);
        healthBarText.text = $"HP {playerDamageable.Health} / {playerDamageable.MaxHealth}";
    }

    void Update()
    {

    }

    private void OnEnable()
    {
        if (playerDamageable != null)
            playerDamageable.healthChanged.AddListener(OnPlayerHealthChanged);
    }

    private void OnDisable()
    {
        if (playerDamageable != null)
            playerDamageable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }

    private float CalculateSliderPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;   
    }

    // Update is called once per frame
    
    private void OnPlayerHealthChanged(float newHealth, float maxHealth)
    {
        healthSlider.value = CalculateSliderPercentage(newHealth, maxHealth);
        healthBarText.text = $"HP {newHealth} / {maxHealth}";
    }
}
