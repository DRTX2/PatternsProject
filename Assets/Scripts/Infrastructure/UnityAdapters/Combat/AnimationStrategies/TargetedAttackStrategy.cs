using UnityEngine;

public class TargetedAttackStrategy : IAnimationAttackStrategy
{
    public void ExecuteAttack(IAnimatorAdapter animator)
    {
        animator.SetBool(AnimationStrings.hasTarget, true);
    }
}
