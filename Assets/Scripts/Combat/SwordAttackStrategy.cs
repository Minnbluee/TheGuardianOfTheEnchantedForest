using UnityEngine;

public class SwordAttackStrategy : IAttackStrategy
{
    public string AttackName => "Espada";

    private readonly float _range;
    private readonly float _damage;
    private readonly LayerMask _enemyLayer;

    public SwordAttackStrategy(float range = 1.2f, float damage = 1f, LayerMask enemyLayer = default)
    {
        _range = range;
        _damage = damage;
        _enemyLayer = enemyLayer;
    }

    public void Execute(Transform origin, Vector2 direction)
    {
        Vector2 attackCenter = (Vector2)origin.position + direction * (_range / 2f);

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackCenter, _range / 2f, _enemyLayer);

        foreach (var hit in hits)
        {
            var enemy = hit.GetComponent<Enemy>();
            enemy?.TakeDamage(_damage);
        }

        AudioManager.Instance?.PlayAttack();

        Debug.Log($"[Strategy] {AttackName} ejecutado en dirección {direction}. Enemigos golpeados: {hits.Length}");
    }
}
