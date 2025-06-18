
using UnityEngine;

public class MeleeAnimationAttackStrategy : IAttackCommnad
{
    public void ExecuteAttack(IAnimatorAdapter animator)
    {
        animator.SetTrigger("attack");
    }
}
