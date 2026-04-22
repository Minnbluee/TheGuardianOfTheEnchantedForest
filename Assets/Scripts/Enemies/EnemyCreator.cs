using UnityEngine;

public static class EnemyCreator
{
    public static Enemy Build<T>(EnemyData data, Vector2 position, bool hasGravity)
        where T : Enemy
    {
        GameObject obj = new GameObject(data.enemyName);
        obj.transform.position = position;
        obj.layer = LayerMask.NameToLayer("Enemy");

        var sr = obj.AddComponent<SpriteRenderer>();
        sr.color = data.color;

        var col = obj.AddComponent<BoxCollider2D>();
        col.size = Vector2.one;

        var rb = obj.AddComponent<Rigidbody2D>();
        rb.gravityScale = hasGravity ? 1f : 0f;
        rb.freezeRotation = true;

        Enemy enemy = obj.AddComponent<T>();
        enemy.Initialize(data);

        Debug.Log($"[AbstractFactory] Creado: {data.enemyName} en {position}");
        return enemy;
    }
}