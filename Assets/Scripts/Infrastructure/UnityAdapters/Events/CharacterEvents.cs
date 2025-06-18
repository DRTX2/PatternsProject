using UnityEngine;
using UnityEngine.Events;

public static class CharacterEvents
{
    public static UnityEvent<GameObject, float, float> OnHealthChanged = new();
    public static UnityEvent<GameObject, float> OnDamageReceived = new();
    public static UnityEvent<GameObject, float> OnHealed = new();
}
