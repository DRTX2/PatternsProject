using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{
    public UnityEvent noCollidersRemain;

    public List<Collider2D> detectedColliders = new List<Collider2D>();
    Collider2D detectionCollider;

    private void Awake()
    {
        detectionCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("CliffDetectionZone detectó: " + collision.name); 
        detectedColliders.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("CliffDetectionZone perdió contacto con: " + collision.name);
        detectedColliders.Remove(collision);

        if(detectedColliders.Count == 0)
        {
            noCollidersRemain?.Invoke();
        } 
    }
}
