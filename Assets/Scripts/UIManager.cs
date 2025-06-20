using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
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
        CharacterEvents.characterDamaged.AddListener(CharacterTookDamage);
        CharacterEvents.characterHealed.AddListener(CharacterHealed);
    }

    private void OnDisable()
    {
        CharacterEvents.characterDamaged.RemoveListener(CharacterTookDamage);
        CharacterEvents.characterHealed.RemoveListener(CharacterHealed);
    }


    public void CharacterTookDamage(GameObject character, int damageReceived)
    {
        // create text at character hit

        Vector3 spawnPosition=Camera.main.WorldToScreenPoint(character.transform.position);
        
        TMP_Text tmpText=Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity,gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = damageReceived.ToString();
    }

    public void CharacterHealed(GameObject character, int healthRestored)
    {
        // create text at character hit

        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = healthRestored.ToString();
    }
}
