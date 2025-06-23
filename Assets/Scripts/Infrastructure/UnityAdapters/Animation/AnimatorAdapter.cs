using UnityEngine;
/// <summary>
/// AnimatorAdapter is a wrapper around Unity's Animator component.
/// Adapter pattern is used to provide a simplified interface for setting and getting animation parameters.
/// </summary>
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

    public bool GetBool(string param)
    {
        return _animator.GetBool(param);
    }

    public float GetFloat(string param)
    {
        return _animator.GetFloat(param);
    }
}
