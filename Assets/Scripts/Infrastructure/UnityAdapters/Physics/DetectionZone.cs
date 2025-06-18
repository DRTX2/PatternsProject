/*using UnityEngine;
using System.Collections.Generic;

public class DetectionZone : MonoBehaviour
{
    [SerializeField] private ContactFilter2D filter;
    [SerializeField] private Collider2D detectionCollider;

    public List<Collider2D> Detected => _detected;
    private List<Collider2D> _detected = new();

    private void FixedUpdate()
    {
        _detected.Clear();
        detectionCollider.Overlap(filter, _detected);
    }
}*/
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public List<Collider2D> detected = new();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!detected.Contains(other))
            detected.Add(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (detected.Contains(other))
            detected.Remove(other);
    }
}
