using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public enum FactoryType { Forest, Corrupt }

    [Header("Familia activa")]
    [SerializeField] private FactoryType activeFactory = FactoryType.Forest;

    [Header("Puntos de Spawn")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnOffsetY = 1f;

    private IEnemyFactory _factory;
    private int _lastSpawnIndex = 0;

    private void Awake()
    {
        SetFactory(activeFactory);
    }

    public void SetFactory(FactoryType type)
    {
        activeFactory = type;
        _factory = type == FactoryType.Forest
            ? (IEnemyFactory)new ForestEnemyFactory()
            : new CorruptEnemyFactory();

        Debug.Log($"[Spawner] Familia activa: {type}");
    }

    public void SpawnMossShade()
    {
        _factory.CreateMossShade(GetNextSpawnPosition());
    }

    public void SpawnFlyingSpore()
    {
        Vector2 pos = GetNextSpawnPosition();
        pos.y += spawnOffsetY;
        _factory.CreateFlyingSpore(pos);
    }

    public void SwitchToForest() => SetFactory(FactoryType.Forest);
    public void SwitchToCorrupt() => SetFactory(FactoryType.Corrupt);

    public void ClearAllEnemies()
    {
        var enemies = FindObjectsByType<Enemy>();
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
        return new Vector2(Random.Range(-4f, 4f), 2f);
    }
}