using UnityEngine;

/// <summary>
/// TargetedAttackStrategy is a concrete implementation of IAnimationAttackStrategy.
/// Used to set the animator's target state for a targeted attack.
/// </summary>
public class TargetedAttackStrategy : IAnimationAttackStrategy
{
    public void ExecuteAttack(IAnimatorAdapter animator)
    {
        animator.SetBool(AnimationStrings.hasTarget, true);
    }
}
