using UnityEngine;
using Zenject;
/// <summary>
/// Handling the attack behavior for the Flying Eye enemy.
/// </summary>
public class FlyingEyeAttackMB : MonoBehaviour, IAttackBehaviour<FlyingEyeEnemy>, ITrackBehaviour<FlyingEyeEnemy>
{
    [Inject] private FlyingEyeEnemy _eye;

    private IAnimatorAdapter _animator;
    private IPhysicsAdapter _physics;

    private void Awake()
    {
        _animator = new AnimatorAdapter(GetComponent<Animator>());
        _physics = new RigidbodyAdapter(GetComponent<Rigidbody2D>());
    }

    private void Update()
    {
        HandleCooldown();
    }

    public void Attack(IAnimationAttackStrategy strategy)
    {
        if (!_eye.IsAlive || _eye.AttackCooldown > 0f) return;

        strategy.ExecuteAttack(_animator);
        _eye.SetAttackCooldown(_animator.GetFloat(AnimationStrings.attackCooldown));
    }

    private void HandleCooldown()
    {
        float currentCooldown = _animator.GetFloat(AnimationStrings.attackCooldown);

        if (currentCooldown > 0f)
        {
            currentCooldown -= Time.deltaTime;
            _eye.SetAttackCooldown(currentCooldown);
            _animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(currentCooldown, 0f));
        }
    }

    public void RegisterTarget()
    {
        _eye.SetTracker(true);
        _animator.SetBool(AnimationStrings.hasTarget, true);
    }

    public void ClearTargets()
    {
        _eye.SetTracker(false);
        _animator.SetBool(AnimationStrings.hasTarget, false);
    }
}
