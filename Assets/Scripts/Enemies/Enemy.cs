using UnityEngine;


public abstract class Enemy : MonoBehaviour
{
    [Header("Stats base")]
    [SerializeField] protected float maxHealth = 3f;
    protected float currentHealth;

    [Header("Puntos al morir")]
    [SerializeField] protected int scoreValue = 100;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
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
        UIManager.Instance?.AddScore(scoreValue);
        Destroy(gameObject);
    }

   
    public abstract void Behave();
}
