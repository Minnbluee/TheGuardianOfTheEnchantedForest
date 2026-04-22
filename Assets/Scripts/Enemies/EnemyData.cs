using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemies/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Identidad")]
    public string enemyName = "Enemigo";

    [Header("Visual")]
    public Sprite sprite;
    public Color color = Color.white;

    [Header("Stats")]
    public float maxHealth = 3f;
    public float moveSpeed = 2f;
    public int scoreValue = 100;
}