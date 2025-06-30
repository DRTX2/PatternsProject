using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CollectScoreUseCase
{
    public void Execute(IScoreCollectibleBehaviour target, int amount)
    {
        if (amount > 0)
        {
            target.CollectScore(amount);
        }
    }
}
