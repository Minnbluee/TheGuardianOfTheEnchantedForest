using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool Instance;

    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private int initialSize = 10;

    private ObjectPool<Projectile> _pool;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        _pool = new ObjectPool<Projectile>(projectilePrefab, transform, initialSize);
    }

    public Projectile Get(Vector2 position) => _pool.Get(position);
    public void Return(Projectile proj) => _pool.Return(proj);
}