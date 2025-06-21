using UnityEngine;
using Zenject;

public class PlayerAttackMB : MonoBehaviour, IAttacker
{
    [Inject] private Player _player;
    private IAnimatorAdapter _animator;

    private void Awake()
    {
        _animator = new AnimatorAdapter(GetComponent<Animator>());
    }

    public void Attack(IAnimationAttackStrategy strategy)
    {
        strategy.ExecuteAttack(_animator);
    }

    public void AttackStart()
    {
        //IsAttacking = true;
    }

    public void AttackEnd()
    {
        //IsAttacking = false;
    }
}
