using UnityEngine;

public class MossShadeEnemy : Enemy
{
    [Header("Patrulla")]
    private float patrolSpeed;
    private float patrolRange = 3f;

    private Vector2 _startPosition;
    private int _direction = 1;

    public override void Initialize(EnemyData data)
    {
        base.Initialize(data);
        _startPosition = transform.position;
        patrolSpeed = data.moveSpeed;
    }

    private void Update()
    {
        Behave();
    }

    public override void Behave()
    {
        transform.Translate(Vector2.right * _direction * patrolSpeed * Time.deltaTime);

        float distanceFromStart = transform.position.x - _startPosition.x;

        if (distanceFromStart >= patrolRange)
            _direction = -1;
        else if (distanceFromStart <= -patrolRange)
            _direction = 1;
    }
}