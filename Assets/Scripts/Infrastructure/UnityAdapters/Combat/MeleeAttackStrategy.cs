
using UnityEngine;

public class MeleeAttackStrategy : IAttackCommnad
{
    public void ExecuteAttack(IAnimatorAdapter animator)
    {
        animator.SetTrigger(AnimationStrings.attackTrigger);
    }
}
