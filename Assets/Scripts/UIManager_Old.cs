using UnityEngine;
using TMPro;

public class UIManager_Old : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;

    public Canvas gameCanvas;

    private void Awake()
    {
        gameCanvas = FindObjectOfType<Canvas>();

    }

    public void OnEnable()
    {
        CharacterEvents_Old.characterDamaged.AddListener(CharacterTookDamage);
        CharacterEvents_Old.characterHealed.AddListener(CharacterHealed);
    }

    private void OnDisable()
    {
        CharacterEvents_Old.characterDamaged.RemoveListener(CharacterTookDamage);
        CharacterEvents_Old.characterHealed.RemoveListener(CharacterHealed);
    }


    public void CharacterTookDamage(GameObject character, int damageReceived)
    {
        // create text at character hit

        Vector3 spawnPosition=Camera.main.WorldToScreenPoint(character.transform.position);
        
        TMP_Text tmpText=Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity,gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = damageReceived.ToString();
        Debug.Log($"[UIManager_Old] Character took damage: {damageReceived} at position: {spawnPosition}");
    }

    public void CharacterHealed(GameObject character, int healthRestored)
    {
        // create text at character hit

        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = healthRestored.ToString();
        Debug.Log($"[UIManager_Old] Character healed: {healthRestored} at position: {spawnPosition}");
    }
}
