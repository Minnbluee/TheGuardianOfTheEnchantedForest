using UnityEngine;

public interface IAttackStrategy
{
    string AttackName { get; }

    void Execute(Transform origin, Vector2 direction);
}
