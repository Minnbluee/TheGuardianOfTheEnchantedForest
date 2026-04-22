using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected EnemyData data;
    protected float currentHealth;

    public virtual void Initialize(EnemyData enemyData)
    {
        data = enemyData;
        currentHealth = data.maxHealth;
        gameObject.name = data.enemyName;

        var sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.sprite = data.sprite;
            sr.color = data.color;
        }
    }

    public virtual void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log($"[Enemy] {gameObject.name} recibió {amount} de daño. Vida restante: {currentHealth}");

        if (currentHealth <= 0)
            Die();
    }

    protected virtual void Die()
    {
        Debug.Log($"[Enemy] {gameObject.name} murió.");
        UIManager.Instance?.AddScore(data.scoreValue);
        Destroy(gameObject);
    }

    public abstract void Behave();
}