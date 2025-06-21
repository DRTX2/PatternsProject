using UnityEngine;

// Uses the collider to check directions to see if the object is currently on the ground,
// _touching the wall, or _touching the ceiling
public class TouchingDirections_Old : MonoBehaviour
{
    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f; // Distance to check for ground contact
    public float wallDistance = 0.02f; // Distance to check for wall contact
    public float ceilingDistance = 0.05f;

    CapsuleCollider2D touchingCol;
    Animator animator;

    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];


    [SerializeField]
    private bool _isGrounded;
    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
        private set
        {
            _isGrounded = value;
            animator.SetBool(AnimationStrings.isGrounded, value);
        }
    }

    [SerializeField]
    private bool _isOnWall;
    public bool IsOnWall
    {
        get
        {
            return _isOnWall;
        }
        private set
        {
            _isOnWall = value;
            animator.SetBool(AnimationStrings.isOnWall, value);
        }
    }

    [SerializeField]
    private bool _isOnCeiling;

    private Vector2 wallCheckDirection => transform.localScale.x > 0 ? Vector2.right : Vector2.left;




    public bool IsOnCeiling
    {
        get
        {
            return _isOnCeiling;
        }
        private set
        {
            _isOnCeiling = value;
            animator.SetBool(AnimationStrings.isOnCeiling, value);
        }
    }

    private void Awake()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();

        // Asegura que se usa LayerMask
        //castFilter.useLayerMask = true;
        //castFilter.useTriggers = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void FixedUpdate()
    {
        int numHits = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance);
        //Debug.Log($"Ground hits: {numHits}");

        foreach (var hit in groundHits)
        {
            if (hit.collider != null)
            {
                //Debug.Log($"Touching: {hit.collider.gameObject.name}");
            }
        }

        IsGrounded = numHits > 0;

        IsOnWall = touchingCol.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = touchingCol.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;
    }

}
