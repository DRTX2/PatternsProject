using System;
using System.Collections.Generic;
using System.Text;
public interface IHealable
{
    float CurrentHealth { get; }
    float MaxHealth { get; }
    bool Heal(float amount);
    bool CanHeal { get; }
}
