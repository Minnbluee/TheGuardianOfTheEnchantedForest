using UnityEngine;

public class MossShadeEnemy : Enemy
{
    private float _patrolRange;
    private Vector2 _startPosition;
    private int _direction = 1;

    public override void Initialize(EnemyData enemyData)
    {
        base.Initialize(enemyData);
        _patrolRange = enemyData.patrolRange;
        _startPosition = transform.position;
        _direction = 1;
    }

    private void Update()
    {
        Behave();
    }

    public override void Behave()
    {
        transform.Translate(Vector2.right * _direction * data.moveSpeed * Time.deltaTime);

        float distanceFromStart = transform.position.x - _startPosition.x;

        if (distanceFromStart >= _patrolRange)
            _direction = -1;
        else if (distanceFromStart <= -_patrolRange)
            _direction = 1;
    }
}