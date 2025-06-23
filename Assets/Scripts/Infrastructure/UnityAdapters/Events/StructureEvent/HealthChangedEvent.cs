using UnityEngine;

/// <summary>
/// HealthChangedEvent is a structure that represents an event when a character's health changes.
/// </summary>
public struct HealthChangedEvent
{
    public GameObject Character;
    public float Current;
    public float Max;
}