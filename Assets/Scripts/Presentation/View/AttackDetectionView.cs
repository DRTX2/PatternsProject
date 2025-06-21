using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

[RequireComponent(typeof(Collider2D))]
public class AttackDetectionView : MonoBehaviour
{
    public UnityEvent OnTargetDetected;
    public UnityEvent NoRemainingTargets;

    public List<Collider2D> _detected = new();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_detected.Contains(other))
        {
            _detected.Add(other);
            if (_detected.Count >= 1)
            {
                OnTargetDetected?.Invoke();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_detected.Contains(other))
        {
            _detected.Remove(other);
            if (_detected.Count == 0)
            {
                NoRemainingTargets?.Invoke();
            }
        }
    }

    public bool HasTarget => _detected.Count > 0;

    private void OnDisable()
    {
        _detected.Clear();
    }
}
