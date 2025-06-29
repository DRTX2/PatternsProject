using UnityEngine;

public class FinishZoneTrigger : MonoBehaviour
{
    [SerializeField] private GameObject finishPanel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (finishPanel != null)
            {
                finishPanel.SetActive(true);
            }
        }
    }
}