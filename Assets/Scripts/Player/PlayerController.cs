using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    

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

    [Header("Movimiento")]
    [SerializeField] private float moveSpeed = 6f;

    [Header("Salto")]
    [SerializeField] private float jumpForce = 14f;

    [Header("Detección de Suelo")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private LayerMask groundLayer;

    

    private Rigidbody2D _rb;
    private PlayerState _currentState;
    private bool _isGrounded;
    private bool _facingRight = true;

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


    private void HandleFSM()
    {
       
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
        
        if (InputHandler.Instance.CrouchHeld)
        {
            ChangeState(PlayerState.Crouch);
            return;
        }

        if (InputHandler.Instance.JumpPressed)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, jumpForce);
            ChangeState(PlayerState.Jump);
            return;
        }

      
        if (InputHandler.Instance.AttackPressed)
        {
            ChangeState(PlayerState.Attack);
            return;
        }

        
        if (Mathf.Abs(InputHandler.Instance.MoveInput) > 0.01f)
            ChangeState(PlayerState.Move);
        else
            ChangeState(PlayerState.Idle);
    }

    private void HandleAirborneStates()
    {
        
        if (_rb.linearVelocity.y < -0.1f)
            ChangeState(PlayerState.Fall);
        else if (_rb.linearVelocity.y > 0.1f)
            ChangeState(PlayerState.Jump);

        
        if (_isGrounded && _rb.linearVelocity.y <= 0)
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
        Debug.Log($"[Player] Estado: {_currentState}"); 
    }


    private void ApplyMovement()
    {
        
        if (_currentState == PlayerState.Attack || _currentState == PlayerState.Hurt)
        {
            _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);
            return;
        }

       
        if (_currentState == PlayerState.Crouch)
        {
            _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);
            return;
        }

        float move = InputHandler.Instance != null ? InputHandler.Instance.MoveInput : 0f;
        _rb.linearVelocity = new Vector2(move * moveSpeed, _rb.linearVelocity.y);

        
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

    

    private void CheckGrounded()
    {
        if (groundCheck == null) return;

        _isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );
    }

    public void EnterHurtState()
    {
        ChangeState(PlayerState.Hurt);
    }

    
    public void ExitHurtState()
    {
        ChangeState(PlayerState.Idle);
    }

    public bool IsGrounded => _isGrounded;
    private PlayerState CurrentState => _currentState;

   

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
