using UnityEngine;

public class AnimatorAdapter : IAnimatorAdapter
{
    private readonly Animator _animator;

    public AnimatorAdapter(Animator animator)
    {
        _animator = animator;
    }

    public void SetBool(string param, bool value)
    {
        _animator.SetBool(param, value);
    }

    public void SetFloat(string param, float value)
    {
        _animator.SetFloat(param, value);
    }

    public void SetTrigger(string param)
    {
        _animator.SetTrigger(param);
    }
}
