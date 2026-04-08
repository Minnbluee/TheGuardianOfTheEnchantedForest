using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance;

    [Header("Input Actions Asset")]
    [SerializeField] private InputActionAsset inputActions;

    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _attackAction;
    private InputAction _crouchAction;

    public float MoveInput { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool AttackPressed { get; private set; }
    public bool CrouchHeld { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SetupActions();
    }

    private void SetupActions()
    {
        if (inputActions == null)
        {
            Debug.LogError("Falta asignar el InputActionAsset en el Inspector.");
            return;
        }

        var map = inputActions.FindActionMap("Player", throwIfNotFound: true);

        _moveAction = map.FindAction("Move", throwIfNotFound: true);
        _jumpAction = map.FindAction("Jump", throwIfNotFound: true);
        _attackAction = map.FindAction("Attack", throwIfNotFound: true);
        _crouchAction = map.FindAction("Crouch", throwIfNotFound: true);

        inputActions.Enable();
    }

    private void Update()
    {
        if (inputActions == null) return;

        MoveInput = _moveAction.ReadValue<Vector2>().x;
        JumpPressed = _jumpAction.WasPressedThisFrame();
        AttackPressed = _attackAction.WasPressedThisFrame();
        CrouchHeld = _crouchAction.IsPressed();
    }

    private void OnDestroy()
    {
        inputActions?.Disable();
    }
}