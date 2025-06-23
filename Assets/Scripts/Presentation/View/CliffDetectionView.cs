using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

/// <summary>
/// CliffDetectionView is responsible for detecting cliffs in the game environment.
/// Represents a view that triggers an event when a cliff is detected or no cliffs remain.
/// </summary>

[RequireComponent(typeof(Collider2D))]
public class CliffDetectionView : MonoBehaviour
{
    public UnityEvent OnCliffDetected;

    private readonly List<Collider2D> _detected = new();

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log($"[NEW] CliffDetectionView detected [NEW]: {other.name}");
        if (!_detected.Contains(other))
        {
            _detected.Add(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_detected.Contains(other))
        {
            _detected.Remove(other);
            if (_detected.Count == 0)
            {
                //Debug.Log($"CliffDetectionView no colliders remain, invoking event.");
                OnCliffDetected?.Invoke();
            }
        }
    }

    private void OnDisable()
    {
        _detected.Clear();
    }
}
