using UnityEngine;
using Zenject;

/// <summary>
/// Handles the attack behavior for the Knight enemy.
/// </summary>

public class KnightAttackMB : MonoBehaviour, IAttackBehaviour<KnightEnemy>, ITrackBehaviour<KnightEnemy>
{
    private IAnimatorAdapter _animator;
    private IPhysicsAdapter _physics;

    [Inject] private KnightEnemy _knight;

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
        if (!_knight.IsAlive || _knight.AttackCooldown > 0f) return;

        strategy.ExecuteAttack(_animator);
        _knight.SetAttackCooldown(_animator.GetFloat(AnimationStrings.attackCooldown));
    }

    private void HandleCooldown()
    {
        float currentCooldown = _animator.GetFloat(AnimationStrings.attackCooldown);

        if (currentCooldown > 0f)
        {
            currentCooldown -= Time.deltaTime;
            _knight.SetAttackCooldown(currentCooldown);
            _animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(currentCooldown, 0f));
        }
    }

    public void RegisterTarget()
    {
        _knight.SetTracker(true);
        _animator.SetBool(AnimationStrings.hasTarget, true);
    }

    public void ClearTargets()
    {
        _knight.SetTracker(false);
        _animator.SetBool(AnimationStrings.hasTarget, false);
    }
}