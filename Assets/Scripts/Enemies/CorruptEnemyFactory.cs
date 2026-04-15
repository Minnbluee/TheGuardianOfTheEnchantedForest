using UnityEngine;

public class CorruptEnemyFactory : IEnemyFactory
{
    public Enemy CreateMossShade(Vector2 position)
    {
        return EnemyCreator.Build<MossShadeEnemy>(
            name: "Musgo [Corrupto]",
            position: position,
            color: new Color(0.7f, 0.1f, 0.1f),
            hasGravity: true
        );
    }

    public Enemy CreateFlyingSpore(Vector2 position)
    {
        return EnemyCreator.Build<FlyingSporeEnemy>(
            name: "Espora [Corrupta]",
            position: position,
            color: new Color(0.9f, 0.4f, 0.0f),
            hasGravity: false
        );
    }
}