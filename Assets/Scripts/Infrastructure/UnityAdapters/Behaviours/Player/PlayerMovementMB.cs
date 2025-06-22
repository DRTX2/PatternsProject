    using UnityEngine;
using Zenject;

public class PlayerMovementMB : MonoBehaviour, IInputMovable
{
    [Inject] private Player _player;

    private IAnimatorAdapter _animator;
    private IPhysicsAdapter _physics;
    private TouchingDirections _touching;

    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float jumpForce = 10f;

    private bool _facingRight = true;

    private void Awake()
    {
        _animator = new AnimatorAdapter(GetComponent<Animator>());
        _physics = new RigidbodyAdapter(GetComponent<Rigidbody2D>());
        _touching = GetComponent<TouchingDirections>();
    }

    public void Move(float x)
    {
        if (!_player.CanMove || (_touching.IsOnWall && !_touching.IsGrounded)) return;
        
        float speed = _player.IsRunning && _touching.IsGrounded ? runSpeed : walkSpeed;
        _physics.SetVelocity(new Vector2(x * speed, _physics.GetVelocity().y));
        _animator.SetBool(AnimationStrings.isMoving, Mathf.Abs(x) > 0.01f);
        _animator.SetFloat(AnimationStrings.yVelocity, _physics.GetVelocity().y);
        FaceDirection(x);
    }

    public void SetRunning(bool isRunning)
    {
        _player.SetRunning(isRunning);
        _animator.SetBool(AnimationStrings.isRunning, isRunning);
    }

    public void Jump()
    {
        if (_touching.IsGrounded && _player.CanMove)
        {
            _physics.SetVelocity(new Vector2(_physics.GetVelocity().x, jumpForce));
            _animator.SetTrigger(AnimationStrings.jumpTrigger);
        }
    }

    public void FaceDirection(float x)
    {
        if (x == 0) return;
        bool shouldFaceRight = x > 0;
        if (shouldFaceRight != _facingRight)
        {
            _facingRight = shouldFaceRight;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
}
