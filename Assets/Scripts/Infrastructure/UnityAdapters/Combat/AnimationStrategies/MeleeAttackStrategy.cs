
using UnityEngine;

/// <summary>
/// MeleeAttackStrategy is a concrete implementation of IAnimationAttackStrategy.
/// Used to execute a melee attack animation on an animator.
/// </summary>
/// 
public class MeleeAttackStrategy : IAnimationAttackStrategy
{
    public void ExecuteAttack(IAnimatorAdapter animator)
    {
        animator.SetTrigger(AnimationStrings.attackTrigger);
    }
}
