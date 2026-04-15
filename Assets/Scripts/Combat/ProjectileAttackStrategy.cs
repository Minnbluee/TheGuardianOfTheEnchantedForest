using UnityEngine;

public class ProjectileAttackStrategy : IAttackStrategy
{
    public string AttackName => "Proyectil";

    private readonly float _speed;
    private readonly float _damage;
    private readonly GameObject _projectilePrefab;

    public ProjectileAttackStrategy(float speed = 10f, float damage = 1f, GameObject projectilePrefab = null)
    {
        _speed = speed;
        _damage = damage;
        _projectilePrefab = projectilePrefab;
    }

    public void Execute(Transform origin, Vector2 direction)
    {
        GameObject projectile;

        if (_projectilePrefab != null)
        {
            projectile = Object.Instantiate(_projectilePrefab, origin.position, Quaternion.identity);
        }
        else
        {
            projectile = new GameObject("Proyectil");
            projectile.transform.position = origin.position;
            projectile.transform.localScale = Vector3.one * 0.3f;

            var sr = projectile.AddComponent<SpriteRenderer>();
            sr.color = Color.red;

            projectile.AddComponent<BoxCollider2D>().isTrigger = true;
        }

        var proj = projectile.AddComponent<Projectile>();
        proj.Initialize(direction, _speed, _damage);

        AudioManager.Instance?.PlayAttack();

        Debug.Log($"[Strategy] {AttackName} disparado en dirección {direction}");
    }
}
