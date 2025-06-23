using UnityEngine;
using Zenject;

/// <summary>
/// Handles player movement, including walking, running, and jumping.
/// </summary>

public class PlayerMovementMB : MonoBehaviour,
    IMoveBehaviour<Player>, IJumpBehaviour<Player>, IRunBehaviour<Player>
{
    [Inject] private Player _player;

    private IAnimatorAdapter _animator;
    private IPhysicsAdapter _physics;
    private SurfaceContactSensor _touching;

    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float jumpForce = 10f;

    private bool _facingRight = true;


    private void Start()
    {
        Vector2 pos = new Vector2(_player.PositionX, _player.PositionY);
        transform.position = pos;
    }

    private void Awake()
    {
        _animator = new AnimatorAdapter(GetComponent<Animator>());
        _physics = new RigidbodyAdapter(GetComponent<Rigidbody2D>());
        _touching = GetComponent<SurfaceContactSensor>();
    }

    public void Move(float x)
    {
        if (!_player.CanMove(_touching.IsGrounded, _touching.IsOnWall)) return;
        
        float speed = _player.IsRunning && _touching.IsGrounded ? runSpeed : walkSpeed;
        _physics.SetVelocity(new Vector2D(x * speed, _physics.GetVelocity().Y));

        _animator.SetBool(AnimationStrings.isMoving, Mathf.Abs(x) > 0.01f);
        _animator.SetFloat(AnimationStrings.yVelocity, _physics.GetVelocity().Y);

        FaceDirection(x);
    }

    public void SetRunning(bool isRunning)
    {
        _player.SetRunning(isRunning);
        _animator.SetBool(AnimationStrings.isRunning, isRunning);
    }

    public void Jump()
    {
        if (_touching.IsGrounded && _player.CanMove(_touching.IsGrounded, _touching.IsOnWall))
        {
            _physics.SetVelocity(new Vector2D(_physics.GetVelocity().X, jumpForce));
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

    private void Update()
    {
        Vector2 pos = transform.position;
        _player.PositionX = pos.x;
        _player.PositionY = pos.y;
    }
}
