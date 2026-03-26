using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    [Header("Puntos de Spawn")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnOffsetY = 1f;

    private int _lastSpawnIndex = 0;

    
    public void SpawnMossShade()
    {
        Vector2 position = GetNextSpawnPosition();
        EnemyFactory.Create(EnemyFactory.EnemyType.MossShade, position);
    }

    public void SpawnFlyingSpore()
    {
        Vector2 position = GetNextSpawnPosition();
        position.y += spawnOffsetY; 
        EnemyFactory.Create(EnemyFactory.EnemyType.FlyingSpore, position);
    }

    public void ClearAllEnemies()
    {
        var enemies = FindObjectsOfType<Enemy>();
        foreach (var enemy in enemies)
            Destroy(enemy.gameObject);

        Debug.Log($"[Spawner] {enemies.Length} enemigos eliminados.");
    }


    private Vector2 GetNextSpawnPosition()
    {
        
        if (spawnPoints != null && spawnPoints.Length > 0)
        {
            Vector2 pos = spawnPoints[_lastSpawnIndex].position;
            _lastSpawnIndex = (_lastSpawnIndex + 1) % spawnPoints.Length;
            return pos;
        }

        float randomX = Random.Range(-4f, 4f);
        return new Vector2(randomX, 2f);
    }
}
