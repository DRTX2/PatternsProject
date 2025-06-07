using UnityEngine;

/// <summary>
/// Controla el comportamiento del enemigo Knight, incluyendo movimiento, detección de objetivos y animaciones.
/// Requiere los componentes Rigidbody2D y TouchingDirections.
/// </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class Knight : MonoBehaviour
{
    /// <summary>
    /// Velocidad de movimiento horizontal del Knight.
    /// </summary>
    public float walkSpeed = 3f;

    /// <summary>
    /// Factor de desaceleración cuando el Knight debe detenerse.
    /// </summary>
    public float walkStopRate = 0.05f;

    /// <summary>
    /// Zona de detección para identificar objetivos cercanos (por ejemplo, el jugador).
    /// </summary>
    public DetectionZone attackZone;

    // Referencia al Rigidbody2D para controlar el movimiento físico.
    Rigidbody2D rb;
    // Referencia al componente TouchingDirections para saber si está tocando suelo, pared o techo.
    TouchingDirections touchingDirections;
    // Referencia al Animator para controlar las animaciones.
    Animator animator;

    /// <summary>
    /// Enum para definir la dirección caminable (izquierda o derecha).
    /// </summary>
    public enum WalkableDirection { Left, Right }

    // Dirección actual del Knight.
    private WalkableDirection _walkDirection;
    // Vector que representa la dirección de movimiento en el eje X.
    private Vector2 walkDirectionVector = Vector2.right;

    /// <summary>
    /// Propiedad para obtener o establecer la dirección de movimiento.
    /// Al cambiar, invierte el sprite y actualiza el vector de dirección.
    /// </summary>
    public WalkableDirection walkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                float newScaleX = (value == WalkableDirection.Right) ? 1f : -1f;
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * newScaleX, transform.localScale.y);
                walkDirectionVector = (value == WalkableDirection.Right) ? Vector2.right : Vector2.left;
            }

            _walkDirection = value;
        }
    }

    /// <summary>
    /// Indica si el Knight tiene un objetivo en su zona de ataque.
    /// </summary>
    public bool _hasTarget = false;

    /// <summary>
    /// Propiedad para saber si hay objetivos detectados.
    /// Actualiza el parámetro del Animator correspondiente.
    /// </summary>
    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value); // Actualiza el Animator
        }
    }

    /// <summary>
    /// Indica si el Knight puede moverse, según el Animator.
    /// </summary>
    public bool CanMove { get { return animator.GetBool(AnimationStrings.canMove); } }

    /// <summary>
    /// Inicializa referencias y parámetros al iniciar el objeto.
    /// </summary>
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        // Inicializa el parámetro de dirección al inicio (opcional, si usas un parámetro de dirección en el Animator)
        animator.SetFloat("moveX", walkDirectionVector.x);
    }

    /// <summary>
    /// Actualiza el estado de HasTarget cada frame.
    /// </summary>
    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
    }

    /// <summary>
    /// Controla el movimiento físico y el cambio de dirección al tocar paredes.
    /// </summary>
    private void FixedUpdate()
    {
        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }

        if (CanMove)
            rb.linearVelocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(Mathf.Lerp(rb.linearVelocity.x, 0, walkStopRate), rb.linearVelocity.y);
    }

    /// <summary>
    /// Cambia la dirección de movimiento del Knight.
    /// </summary>
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
        else
        {
            Debug.LogError("Invalid walk direction set. Cannot flip direction.");
        }
    }
}
