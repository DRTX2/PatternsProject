using System;
using System.Collections.Generic;
using System.Text;

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
    public void ForceSetCurrent(float value)
    {
        Current = Math.Clamp(value, 0, Max);
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