using UnityEngine;

/// <summary>
/// Controla el movimiento del jugador usando una Máquina de Estados (FSM).
/// Lee las entradas del InputHandler y aplica física con Rigidbody2D.
///
/// Setup en Unity:
///   - Agregar este script al GameObject del jugador.
///   - El jugador necesita: Rigidbody2D, Collider2D, y este script.
///   - Configurar Rigidbody2D: Gravity Scale = 3, Freeze Rotation Z = true.
///   - Asignar el Layer "Ground" en el Inspector para la detección de suelo.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    // ─── Estados de la FSM ─────────────────────────────────────────────────────

    private enum PlayerState
    {
        Idle,
        Move,
        Jump,
        Fall,
        Attack,
        Crouch,
        Hurt
    }

    // ─── Parámetros configurables en el Inspector ──────────────────────────────

    [Header("Movimiento")]
    [SerializeField] private float moveSpeed = 6f;

    [Header("Salto")]
    [SerializeField] private float jumpForce = 14f;

    [Header("Detección de Suelo")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private LayerMask groundLayer;

    // ─── Referencias internas ──────────────────────────────────────────────────

    private Rigidbody2D _rb;
    private PlayerState _currentState;
    private bool _isGrounded;
    private bool _facingRight = true;

    // ─── Unity Lifecycle ───────────────────────────────────────────────────────

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ChangeState(PlayerState.Idle);
    }

    private void Update()
    {
        CheckGrounded();
        HandleFSM();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    // ─── FSM ───────────────────────────────────────────────────────────────────

    private void HandleFSM()
    {
        // El estado Hurt solo se sale desde afuera (via TakeDamage)
        if (_currentState == PlayerState.Hurt) return;

        switch (_currentState)
        {
            case PlayerState.Idle:
            case PlayerState.Move:
                HandleGroundedStates();
                break;

            case PlayerState.Jump:
            case PlayerState.Fall:
                HandleAirborneStates();
                break;

            case PlayerState.Crouch:
                HandleCrouchState();
                break;
        }
    }

    private void HandleGroundedStates()
    {
        // Agacharse tiene prioridad
        if (InputHandler.Instance.CrouchHeld)
        {
            ChangeState(PlayerState.Crouch);
            return;
        }

        // Saltar
        if (InputHandler.Instance.JumpPressed)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            ChangeState(PlayerState.Jump);
            return;
        }

        // Atacar (bloquea movimiento horizontal)
        if (InputHandler.Instance.AttackPressed)
        {
            ChangeState(PlayerState.Attack);
            return;
        }

        // Moverse o quedarse quieto
        if (Mathf.Abs(InputHandler.Instance.MoveInput) > 0.01f)
            ChangeState(PlayerState.Move);
        else
            ChangeState(PlayerState.Idle);
    }

    private void HandleAirborneStates()
    {
        // Subiendo → Jump, bajando → Fall
        if (_rb.velocity.y < -0.1f)
            ChangeState(PlayerState.Fall);
        else if (_rb.velocity.y > 0.1f)
            ChangeState(PlayerState.Jump);

        // Aterrizó
        if (_isGrounded && _rb.velocity.y <= 0)
        {
            ChangeState(Mathf.Abs(InputHandler.Instance.MoveInput) > 0.01f
                ? PlayerState.Move
                : PlayerState.Idle);
        }
    }

    private void HandleCrouchState()
    {
        if (!InputHandler.Instance.CrouchHeld)
        {
            ChangeState(PlayerState.Idle);
        }
    }

    private void ChangeState(PlayerState newState)
    {
        if (_currentState == newState) return;

        _currentState = newState;
        Debug.Log($"[Player] Estado: {_currentState}"); // Útil para debug
    }

    // ─── Movimiento físico ─────────────────────────────────────────────────────

    private void ApplyMovement()
    {
        // Durante Attack o Hurt no hay movimiento horizontal
        if (_currentState == PlayerState.Attack || _currentState == PlayerState.Hurt)
        {
            _rb.velocity = new Vector2(0, _rb.velocity.y);
            return;
        }

        // Durante Crouch tampoco
        if (_currentState == PlayerState.Crouch)
        {
            _rb.velocity = new Vector2(0, _rb.velocity.y);
            return;
        }

        float move = InputHandler.Instance != null ? InputHandler.Instance.MoveInput : 0f;
        _rb.velocity = new Vector2(move * moveSpeed, _rb.velocity.y);

        // Girar el sprite según la dirección
        if (move > 0 && !_facingRight) Flip();
        else if (move < 0 && _facingRight) Flip();
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // ─── Detección de suelo ────────────────────────────────────────────────────

    private void CheckGrounded()
    {
        if (groundCheck == null) return;

        _isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );
    }

    // ─── API pública (para sistema de daño, más adelante) ─────────────────────

    /// <summary>Llamar desde el sistema de daño cuando el jugador recibe un golpe.</summary>
    public void EnterHurtState()
    {
        ChangeState(PlayerState.Hurt);
    }

    /// <summary>Llamar cuando termina la animación de hurt.</summary>
    public void ExitHurtState()
    {
        ChangeState(PlayerState.Idle);
    }

    public bool IsGrounded => _isGrounded;
    public PlayerState CurrentState => _currentState;

    // ─── Gizmos (visualización en el editor) ──────────────────────────────────

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
