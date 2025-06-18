using UnityEngine;
using Zenject;

public class PlayerAttackMB : MonoBehaviour, IAttacker
{
    private IAnimatorAdapter _animator;

    private void Awake()
    {
        _animator = new AnimatorAdapter(GetComponent<Animator>());
    }

    public void Attack(IAttackCommnad strategy)
    {
        strategy.ExecuteAttack(_animator);
    }
}
