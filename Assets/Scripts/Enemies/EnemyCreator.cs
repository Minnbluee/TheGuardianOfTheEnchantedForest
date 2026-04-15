using UnityEngine;

public static class EnemyCreator
{
    public static Enemy Build<T>(string name, Vector2 position, Color color, bool hasGravity)
        where T : Enemy
    {
        GameObject obj = new GameObject(name);
        obj.transform.position = position;
        obj.layer = LayerMask.NameToLayer("Enemy");

        var sr = obj.AddComponent<SpriteRenderer>();
        sr.color = color;

        var col = obj.AddComponent<BoxCollider2D>();
        col.size = Vector2.one;

        var rb = obj.AddComponent<Rigidbody2D>();
        rb.gravityScale = hasGravity ? 1f : 0f;
        rb.freezeRotation = true;

        Enemy enemy = obj.AddComponent<T>();
        Debug.Log($"[AbstractFactory] Creado: {name} en {position}");
        return enemy;
    }
}