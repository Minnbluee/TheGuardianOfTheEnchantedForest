using UnityEngine;

public class ForestEnemyFactory : MonoBehaviour, IEnemyFactory
{
    [Header("Assets de enemigos - Familia Bosque")]
    [SerializeField] private EnemyData mossShadeData;
    [SerializeField] private EnemyData flyingSporeData;

    public Enemy CreateMossShade(Vector2 position)
    {
        return EnemyCreator.Build<MossShadeEnemy>(mossShadeData, position, hasGravity: true);
    }

    public Enemy CreateFlyingSpore(Vector2 position)
    {
        return EnemyCreator.Build<FlyingSporeEnemy>(flyingSporeData, position, hasGravity: false);
    }
}