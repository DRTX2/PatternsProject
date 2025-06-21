using UnityEngine;
using Zenject;

/// <summary>
/// Componente de movimiento autónomo para enemigos tipo Knight.
/// </summary>
[RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(TouchingDirections))]
public class KnightMovementMB : MonoBehaviour, IAutonomousMovable
{
    [Inject] private KnightEnemy _knight;

    private IAnimatorAdapter _animator;
    private IPhysicsAdapter _physics;
    private TouchingDirections _touching;

    private float _lastFlipTime = -999f;
    private const float FlipCooldown = 0.5f;

    private void Awake()
    {
        _animator = new AnimatorAdapter(GetComponent<Animator>());
        _physics = new RigidbodyAdapter(GetComponent<Rigidbody2D>());
        _touching = GetComponent<TouchingDirections>();
    }

    public void Move()
    {
        TryFlipOnWall();

        if (_knight.CanMove && !_animator.GetBool(AnimationStrings.lockVelocity))
        {
            float speedX = _knight.HorizontalSpeed;
            ApplyHorizontalMovement(speedX);
        }
        else
        {
            Decelerate();
        }
    }

    public void FlipDirection()
    {
        _knight.FlipDirection();
    }

    private void TryFlipOnWall()
    {
        if (_touching.IsGrounded && _touching.IsOnWall && Time.time > _lastFlipTime + FlipCooldown)
        {
            FlipDirection();
            _lastFlipTime = Time.time;
            Debug.Log("Knight flipped direction due to wall collision.");
        }
    }

    private void ApplyHorizontalMovement(float speedX)
    {
        _physics.SetVelocity(new Vector2(speedX, _physics.GetVelocity().y));
        _animator.SetFloat(AnimationStrings.xMove, speedX);
        FlipVisual(speedX);
    }

    private void Decelerate()
    {
        float decelX = Mathf.Lerp(_physics.GetVelocity().x, 0, _knight.WalkStopRate);
        _physics.SetVelocity(new Vector2(decelX, _physics.GetVelocity().y));
        _animator.SetFloat(AnimationStrings.xMove, decelX);
    }

    private void FlipVisual(float xDir)
    {
        float scaleX = transform.localScale.x;
        bool shouldFlip = (xDir < 0 && scaleX > 0) || (xDir > 0 && scaleX < 0);

        if (shouldFlip)
        {
            transform.localScale = new Vector3(-scaleX, transform.localScale.y, transform.localScale.z);
        }
    }

    public void DisableMovement()
    {
        _knight.SetCanMove(false);
        _animator.SetBool(AnimationStrings.canMove, false);
    }

    public void EnableMovement()
    {
        _knight.SetCanMove(true);
        _animator.SetBool(AnimationStrings.canMove, true);
    }
}