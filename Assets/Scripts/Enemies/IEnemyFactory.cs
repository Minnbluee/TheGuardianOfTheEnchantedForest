public interface IEnemyFactory
{
    Enemy CreateMossShade(UnityEngine.Vector2 position);
    Enemy CreateFlyingSpore(UnityEngine.Vector2 position);
}