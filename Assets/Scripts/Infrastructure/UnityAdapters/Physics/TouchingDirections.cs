using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D), typeof(Animator))]
public class TouchingDirections : MonoBehaviour
{
    [Header("Raycast Settings")]
    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f;
    public float wallDistance = 0.02f;
    public float ceilingDistance = 0.05f;

    public bool IsGrounded { get; private set; }
    public bool IsOnWall { get; private set; }
    public bool IsOnCeiling { get; private set; }

    private CapsuleCollider2D _collider;
    private Animator _animator;
    private RaycastHit2D[] _hits = new RaycastHit2D[5];

    private Vector2 WallCheckDirection => transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    private void Awake()
    {
        _collider = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        bool previousGrounded = IsGrounded;
        bool previousWall = IsOnWall;
        bool previousCeiling = IsOnCeiling;

        IsGrounded = _collider.Cast(Vector2.down, castFilter, _hits, groundDistance) > 0;
        IsOnWall = _collider.Cast(WallCheckDirection, castFilter, _hits, wallDistance) > 0;
        IsOnCeiling = _collider.Cast(Vector2.up, castFilter, _hits, ceilingDistance) > 0;

        _animator.SetBool(AnimationStrings.isGrounded, IsGrounded);
        _animator.SetBool(AnimationStrings.isOnWall, IsOnWall);
        _animator.SetBool(AnimationStrings.isOnCeiling, IsOnCeiling);
    }
}
