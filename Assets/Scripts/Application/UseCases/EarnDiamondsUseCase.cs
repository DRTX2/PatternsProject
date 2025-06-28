using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EarnDiamondsUseCase
{
    public void ExecuteHeal(IHealBehaviour healer, float amount)
    {
        return healer.Heal(amount);
    }
}
