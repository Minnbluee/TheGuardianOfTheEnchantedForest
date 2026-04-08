using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float moveSpeed = 6f;

    [Header("Salto")]
    [SerializeField] private float jumpForce = 14f;

    [Header("Detección de Suelo")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private LayerMask groundLayer;

    // Estados
    public IPlayerState IdleState { get; private set; }
    public IPlayerState MoveState { get; private set; }
    public IPlayerState JumpState { get; private set; }
    public IPlayerState FallState { get; private set; }
    public IPlayerState CrouchState { get; private set; }
    public IPlayerState AttackState { get; private set; }
    public IPlayerState HurtState { get; private set; }

    private IPlayerState _currentState;
    private Rigidbody2D _rb;
    private bool _facingRight = true;

    
    public float MoveInput => InputHandler.Instance != null ? InputHandler.Instance.MoveInput : 0f;
    public bool JumpPressed => InputHandler.Instance != null && InputHandler.Instance.JumpPressed;
    public bool AttackPressed => InputHandler.Instance != null && InputHandler.Instance.AttackPressed;
    public bool CrouchHeld => InputHandler.Instance != null && InputHandler.Instance.CrouchHeld;
    public float VerticalVelocity => _rb.linearVelocity.y;
    public bool IsGrounded { get; private set; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        IdleState = new PlayerIdleState();
        MoveState = new PlayerMoveState();
        JumpState = new PlayerJumpState();
        FallState = new PlayerFallState();
        CrouchState = new PlayerCrouchState();
        AttackState = new PlayerAttackState();
        HurtState = new PlayerHurtState();
    }

    private void Start()
    {
        ChangeState(IdleState);
    }

    private void Update()
    {
        CheckGrounded();
        _currentState?.Update(this);
    }

    private void FixedUpdate()
    {
        _currentState?.FixedUpdate(this);
    }

    public void ChangeState(IPlayerState newState)
    {
        if (_currentState == newState) return;
        _currentState?.Exit(this);
        _currentState = newState;
        _currentState.Enter(this);
    }

    // Métodos de movimiento que usan los estados
    public void ApplyHorizontalVelocity(float input)
    {
        _rb.linearVelocity = new Vector2(input * moveSpeed, _rb.linearVelocity.y);
    }

    public void ApplyJumpForce()
    {
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpForce);
    }

    public void HandleFlip()
    {
        if (MoveInput > 0 && !_facingRight) Flip();
        else if (MoveInput < 0 && _facingRight) Flip();
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void CheckGrounded()
    {
        if (groundCheck == null) return;
        IsGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    // Métodos públicos que usa PlayerAttack (TP2) — no cambian
    public void EnterHurtState() => ChangeState(HurtState);
    public void ExitHurtState() => ChangeState(IdleState);

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}