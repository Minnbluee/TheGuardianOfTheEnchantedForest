using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private GameObject projectilePrefab; 

    private IAttackStrategy _currentStrategy;

    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();

        SetStrategy(new SwordAttackStrategy(enemyLayer: enemyLayer));
    }

    private void Update()
    {
        if (InputHandler.Instance == null) return;
        if (_playerController == null) return;

        if (InputHandler.Instance.AttackPressed)
        {
            Vector2 direction = transform.localScale.x > 0
                ? Vector2.right
                : Vector2.left;

            _currentStrategy.Execute(transform, direction);
        }
    }

    public void SetStrategy(IAttackStrategy strategy)
    {
        _currentStrategy = strategy;
        Debug.Log($"[PlayerAttack] Estrategia cambiada a: {strategy.AttackName}");
    }

    public void SwitchToSword()
    {
        SetStrategy(new SwordAttackStrategy(enemyLayer: enemyLayer));
    }

    public void SwitchToProjectile()
    {
        SetStrategy(new ProjectileAttackStrategy(projectilePrefab: projectilePrefab));
    }

    public string CurrentStrategyName => _currentStrategy?.AttackName ?? "Ninguna";
}
