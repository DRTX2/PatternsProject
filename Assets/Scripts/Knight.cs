using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections) )]
public class Knight : MonoBehaviour
{

    public float walkSpeed = 3f;
    Rigidbody2D rb;
    TouchingDirections touchingDirections;

    public enum WalkableDirection { Left, Right }

    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector=Vector2.right;

    public WalkableDirection walkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value) // flip direction
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                
                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector=Vector2.right;
                }else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }

            _walkDirection = value;
        }
    }

    private void Awake()
    {
        rb= GetComponent<Rigidbody2D>();
        touchingDirections= GetComponent<TouchingDirections>();
    }

    private void FixedUpdate()
    {
        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            // solo gira si ya est� caminando al menos una distancia m�nima
            //if (Mathf.Abs(rb.linearVelocity.x) > 0.1f)
            //{
                FlipDirection();
            //}
        }
        rb.linearVelocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.linearVelocity.y);
    }

    private void FlipDirection()
    {
        if (walkDirection == WalkableDirection.Right)
        {
            walkDirection = WalkableDirection.Left;
        }
        else if (walkDirection == WalkableDirection.Left)
        {
            walkDirection = WalkableDirection.Right;
        }
        else { 
            Debug.LogError("Invalid walk direction set. Cannot flip direction.");
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        walkDirection = WalkableDirection.Left;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
