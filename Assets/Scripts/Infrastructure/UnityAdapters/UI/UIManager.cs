using UnityEngine;
using TMPro;
using Zenject;

/// <summary>
/// UIManager is responsible for displaying UI elements such as damage and health text.
/// </summary>
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject damageTextPrefab;
    [SerializeField] private GameObject healthTextPrefab;
    [SerializeField] private Canvas gameCanvas;

    [Inject] private CharacterEventBus _eventBus;

    private void Awake()
    {
        gameCanvas = FindFirstObjectByType<Canvas>();

    }

    private void OnEnable()
    {
        _eventBus.DamageReceived.Subscribe(ShowDamageText);
        _eventBus.Healed.Subscribe(ShowHealText);
    }

    private void OnDisable()
    {
        _eventBus.DamageReceived.Unsubscribe(ShowDamageText);
        _eventBus.Healed.Unsubscribe(ShowHealText);
    }

    private void ShowDamageText(DamageEvent e)
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(e.Character.transform.position);
        TMP_Text text = Instantiate(damageTextPrefab, pos, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        text.text = $"-{e.Amount}";
    }

    private void ShowHealText(HealEvent e)
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(e.Character.transform.position);
        TMP_Text text = Instantiate(healthTextPrefab, pos, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        text.text = $"+{e.Amount}";
    }
}
