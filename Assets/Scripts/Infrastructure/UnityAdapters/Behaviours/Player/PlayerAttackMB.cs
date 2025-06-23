using UnityEngine;
using Zenject;

/// <summary>
/// Component for handling player attack behavior.
/// </summary>

public class PlayerAttackMB : MonoBehaviour, IAttackBehaviour<Player>
{
    [Inject] public Player _player;
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
        //_player.SetAttacking(true);
    }

    public void AttackEnd()
    {
        //_player.SetAttacking(false);
    }
}


