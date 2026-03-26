using UnityEngine;


public class FlyingSporeEnemy : Enemy
{
    [Header("Movimiento")]
    [SerializeField] private float horizontalSpeed = 2f;
    [SerializeField] private float waveAmplitude = 1.5f;
    [SerializeField] private float waveFrequency = 2f;

    private Vector2 _startPosition;
    private float _timeOffset;

    protected override void Awake()
    {
        base.Awake();
        _startPosition = transform.position;
        _timeOffset = Random.Range(0f, Mathf.PI * 2f); 

        var sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.color = new Color(0.6f, 0.1f, 0.8f);
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
