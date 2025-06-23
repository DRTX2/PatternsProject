using UnityEngine;
using UnityEngine.TextCore.Text;
using Zenject;

/// <summary>
/// Component for autonomous movement of Knight enemies.
/// </summary>

[RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(SurfaceContactSensor))]
public class KnightMovementMB : MonoBehaviour, IMoveBehaviour<KnightEnemy>
{
    [Inject] private KnightEnemy _knight;

    private IAnimatorAdapter _animator;
    private IPhysicsAdapter _physics;
    private SurfaceContactSensor _touching;

    private float _lastFlipTime = -999f;
    private const float FlipCooldown = 0.5f;

    private void Awake()
    {
        _animator = new AnimatorAdapter(GetComponent<Animator>());
        _physics = new RigidbodyAdapter(GetComponent<Rigidbody2D>());
        _touching = GetComponent<SurfaceContactSensor>();
    }

    public void Move(float directionMultiplier)
    {
        TryFlipOnWall();

        if (_knight.CanMove(_touching.IsGrounded, _touching.IsOnWall))
        {
            float speedX = _knight.HorizontalSpeed * directionMultiplier;
            _physics.SetVelocity(new Vector2D(speedX, _physics.GetVelocity().Y));
            _animator.SetFloat(AnimationStrings.xMove, speedX);
            FaceDirection(speedX);
        }
        else
        {
            Decelerate();
        }
    }

    public void FaceDirection(float x)
    {
        if (x == 0) return;

        float scaleX = transform.localScale.x;
        bool shouldFlip = (x < 0 && scaleX > 0) || (x > 0 && scaleX < 0);

        if (shouldFlip)
        {
            transform.localScale = new Vector3(-scaleX, transform.localScale.y, transform.localScale.z);
        }
        //_knight.FlipDirection();
    }

    private void TryFlipOnWall()
    {
        if (_touching.IsGrounded && _touching.IsOnWall && Time.time > _lastFlipTime + FlipCooldown)
        {
            _knight.FlipDirection();
            _lastFlipTime = Time.time;
        }
    }

    private void Decelerate()
    {
        float decelX = Mathf.Lerp(_physics.GetVelocity().X, 0, _knight.WalkStopRate);
        _physics.SetVelocity(new Vector2D(decelX, _physics.GetVelocity().Y));
        _animator.SetFloat(AnimationStrings.xMove, decelX);
    }

    public void DisableMovement()
    {
        _knight.SetLockVelocity(true);
        _animator.SetBool(AnimationStrings.canMove, false);
    }

    public void EnableMovement()
    {
        _knight.SetLockVelocity(false);
        _animator.SetBool(AnimationStrings.canMove, true);
    }
}
