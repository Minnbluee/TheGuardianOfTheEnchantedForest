using UnityEngine;

public class FlyingSporeEnemy : Enemy
{
    [Header("Movimiento")]
    private float horizontalSpeed;
    private float waveAmplitude = 1.5f;
    private float waveFrequency = 2f;

    private Vector2 _startPosition;
    private float _timeOffset;

    public override void Initialize(EnemyData data)
    {
        base.Initialize(data);
        _startPosition = transform.position;
        _timeOffset = Random.Range(0f, Mathf.PI * 2f);
        horizontalSpeed = data.moveSpeed;
    }

    private void Update()
    {
        Behave();
    }

    public override void Behave()
    {
        float newX = transform.position.x + horizontalSpeed * Time.deltaTime;
        float newY = _startPosition.y + Mathf.Sin((Time.time + _timeOffset) * waveFrequency) * waveAmplitude;

        transform.position = new Vector2(newX, newY);
    }
}