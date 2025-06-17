using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject damageTextPrefab;
    [SerializeField] private GameObject healthTextPrefab;
    [SerializeField] private Canvas gameCanvas;

    private void Awake()
    {
        gameCanvas = FindFirstObjectByType<Canvas>();

    }

    private void OnEnable()
    {
        CharacterEvents.OnDamageReceived.AddListener(ShowDamageText);
        CharacterEvents.OnHealed.AddListener(ShowHealText);
    }

    private void OnDisable()
    {
        CharacterEvents.OnDamageReceived.RemoveListener(ShowDamageText);
        CharacterEvents.OnHealed.RemoveListener(ShowHealText);
    }

    private void ShowDamageText(GameObject target, float damage)
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(target.transform.position);
        TMP_Text text = Instantiate(damageTextPrefab, pos, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        text.text = $"-{damage}";
        Debug.Log($"[UIManager] Character took damage: {damage} at position: {pos}"); // Debug log for damage
    }

    private void ShowHealText(GameObject target, float amount)
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(target.transform.position);
        TMP_Text text = Instantiate(healthTextPrefab, pos, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        text.text = $"+{amount}";
        Debug.Log($"[UIManager] Character healed: {amount} at position: {pos}"); // Debug log for healing
    }
}
