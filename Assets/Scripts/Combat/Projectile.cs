using UnityEngine;


public class Projectile : MonoBehaviour
{
    private Vector2 _direction;
    private float _speed;
    private float _damage;
    private float _lifetime = 3f;

    public void Initialize(Vector2 direction, float speed, float damage)
    {
        _direction = direction.normalized;
        _speed = speed;
        _damage = damage;

        Destroy(gameObject, _lifetime);
    }

    private void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
