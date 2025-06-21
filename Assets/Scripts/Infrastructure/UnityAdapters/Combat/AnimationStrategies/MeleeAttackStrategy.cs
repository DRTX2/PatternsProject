
using UnityEngine;

public class MeleeAttackStrategy : IAnimationAttackStrategy
{
    public void ExecuteAttack(IAnimatorAdapter animator)
    {
        animator.SetTrigger(AnimationStrings.attackTrigger);
    }
}
