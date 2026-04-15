using UnityEngine;

public class ForestEnemyFactory : IEnemyFactory
{
    public Enemy CreateMossShade(Vector2 position)
    {
        return EnemyCreator.Build<MossShadeEnemy>(
            name: "Musgo [Bosque]",
            position: position,
            color: new Color(0.2f, 0.6f, 0.2f),
            hasGravity: true
        );
    }

    public Enemy CreateFlyingSpore(Vector2 position)
    {
        return EnemyCreator.Build<FlyingSporeEnemy>(
            name: "Espora [Bosque]",
            position: position,
            color: new Color(0.6f, 0.2f, 0.8f),
            hasGravity: false
        );
    }
}