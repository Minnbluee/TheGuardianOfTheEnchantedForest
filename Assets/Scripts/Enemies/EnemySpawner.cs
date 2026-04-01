using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    [Header("Puntos de Spawn")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnOffsetY = 1f;

    private int _lastSpawnIndex = 0;
    private List<Enemy> _enemies = new List<Enemy>();

    public void SpawnMossShade()
    {
        Vector2 position = GetNextSpawnPosition();
        Enemy enemy = EnemyFactory.Create(EnemyFactory.EnemyType.MossShade, position);
        _enemies.Add(enemy);
    }

    public void SpawnFlyingSpore()
    {
        Vector2 position = GetNextSpawnPosition();
        position.y += spawnOffsetY;

        Enemy enemy = EnemyFactory.Create(EnemyFactory.EnemyType.FlyingSpore, position);
        _enemies.Add(enemy);
    }

    public void ClearAllEnemies()
    {
        foreach (var enemy in _enemies)
        {
            if (enemy != null)
                Destroy(enemy.gameObject);
        }

        _enemies.Clear();

        Debug.Log("[Spawner] Enemigos eliminados.");
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
