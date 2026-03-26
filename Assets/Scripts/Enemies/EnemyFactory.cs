using UnityEngine;


public static class EnemyFactory
{
    public enum EnemyType
    {
        MossShade,
        FlyingSpore
    }

   
    public static Enemy Create(EnemyType type, Vector2 position)
    {
        GameObject enemyObject = new GameObject();
        enemyObject.transform.position = position;

        
        enemyObject.AddComponent<SpriteRenderer>();
        enemyObject.layer = LayerMask.NameToLayer("Enemy");


        var collider = enemyObject.AddComponent<BoxCollider2D>();
        collider.size = Vector2.one;

        Enemy enemy;

        switch (type)
        {
            case EnemyType.MossShade:
                enemyObject.name = "Sombra de Musgo";
                enemy = enemyObject.AddComponent<MossShadeEnemy>();

                
                var rb = enemyObject.AddComponent<Rigidbody2D>();
                rb.freezeRotation = true;
                break;

            case EnemyType.FlyingSpore:
                enemyObject.name = "Espora Voladora";
                enemy = enemyObject.AddComponent<FlyingSporeEnemy>();

                
                var rbFlying = enemyObject.AddComponent<Rigidbody2D>();
                rbFlying.gravityScale = 0f;
                rbFlying.freezeRotation = true;
                break;

            default:
                Debug.LogError($"[EnemyFactory] Tipo de enemigo desconocido: {type}");
                Object.Destroy(enemyObject);
                return null;
        }

        Debug.Log($"[Factory] Enemigo creado: {enemyObject.name} en {position}");
        return enemy;
    }
}
