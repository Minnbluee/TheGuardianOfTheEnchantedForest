using UnityEngine;

public class FlyingSporeEnemy : Enemy
{
    private Vector2 _startPosition;
    private float _timeOffset;

    public override void Initialize(EnemyData enemyData)
    {
        base.Initialize(enemyData);
        _startPosition = transform.position;
        _timeOffset = Random.Range(0f, Mathf.PI * 2f);
    }

    private void Update()
    {
        Behave();
    }

    public override void Behave()
    {
        float newX = transform.position.x + data.moveSpeed * Time.deltaTime;
        float newY = _startPosition.y + Mathf.Sin((Time.time + _timeOffset) * data.waveFrequency) * data.waveAmplitude;

        transform.position = new Vector2(newX, newY);
    }
}