using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Captura todas las entradas del jugador y las expone como propiedades simples.
/// El resto del juego lee de acá, sin saber nada del Input System de Unity.
///
/// Setup en Unity:
///   - Agregar este script al GameObject _Managers en MainMenu.
///   - Asignar el PlayerInputActions asset en el Inspector.
/// </summary>
public class InputHandler : Singleton<InputHandler>
{
    [Header("Input Actions Asset")]
    [SerializeField] private InputActionAsset inputActions;

    // Acciones individuales
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _attackAction;
    private InputAction _crouchAction;

    // ─── Propiedades públicas (el resto del juego lee estas) ───────────────────

    /// <summary>Valor horizontal de movimiento. -1 izquierda, 0 quieto, 1 derecha.</summary>
    public float MoveInput { get; private set; }

    /// <summary>True en el frame exacto que se presiona saltar.</summary>
    public bool JumpPressed { get; private set; }

    /// <summary>True en el frame exacto que se presiona atacar.</summary>
    public bool AttackPressed { get; private set; }

    /// <summary>True mientras se mantiene presionado agacharse.</summary>
    public bool CrouchHeld { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        if (Instance != this) return;

        SetupActions();
    }

    private void SetupActions()
    {
        if (inputActions == null)
        {
            Debug.LogError("[InputHandler] No se asignó el InputActionAsset en el Inspector.");
            return;
        }

        var map = inputActions.FindActionMap("Player", throwIfNotFound: true);

        _moveAction   = map.FindAction("Move",   throwIfNotFound: true);
        _jumpAction   = map.FindAction("Jump",   throwIfNotFound: true);
        _attackAction = map.FindAction("Attack", throwIfNotFound: true);
        _crouchAction = map.FindAction("Crouch", throwIfNotFound: true);

        inputActions.Enable();
    }

    private void Update()
    {
        if (inputActions == null) return;

        // Movimiento horizontal: devuelve un Vector2, tomamos solo X
        MoveInput = _moveAction.ReadValue<Vector2>().x;

        // Pressed: solo true en el frame que se apreta
        JumpPressed   = _jumpAction.WasPressedThisFrame();
        AttackPressed = _attackAction.WasPressedThisFrame();

        // Held: true mientras se mantiene
        CrouchHeld = _crouchAction.IsPressed();
    }

    private void OnDestroy()
    {
        inputActions?.Disable();
    }
}
