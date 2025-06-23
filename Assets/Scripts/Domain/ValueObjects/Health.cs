using System;
using System.Collections.Generic;
using System.Text;
/// <summary>
/// Value Object representing the health of an entity.
/// </summary>
public class Health
{
    public float Current { get; private set; }
    public float Max { get; private set; }

    public bool CanHeal => Current < Max;

    public Health(float max)
    {
        Max = max;
        Current = max;
    }

    public void Reduce(float amount)
    {
        Current = System.Math.Max(Current - amount, 0);
    }

    public void Restore(float amount)
    {
        Current = System.Math.Min(Current + amount, Max);
    }
}