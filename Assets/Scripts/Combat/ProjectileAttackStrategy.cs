using UnityEngine;

public class ProjectileAttackStrategy : IAttackStrategy
{
    public string AttackName => "Proyectil";

    private readonly float _speed;
    private readonly float _damage;

    public ProjectileAttackStrategy(float speed = 10f, float damage = 1f, GameObject projectilePrefab = null)
    {
        _speed = speed;
        _damage = damage;
    }

    public void Execute(Transform origin, Vector2 direction)
    {
        if (ProjectilePool.Instance == null)
        {
            Debug.LogWarning("[Strategy] No hay ProjectilePool en la escena.");
            return;
        }

        Projectile proj = ProjectilePool.Instance.Get(origin.position);
        proj.Initialize(direction.normalized, _speed, _damage);

        AudioManager.Instance?.PlayAttack();
        Debug.Log($"[Strategy] {AttackName} disparado en dirección {direction}");
    }
}