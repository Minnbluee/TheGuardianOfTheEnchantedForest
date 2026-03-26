using UnityEngine;

public class MossShadeEnemy : Enemy
{
    [Header("Patrulla")]
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float patrolRange = 3f;

    private Vector2 _startPosition;
    private int _direction = 1;

    protected override void Awake()
    {
        base.Awake();
        _startPosition = transform.position;

        var sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.color = new Color(0.1f, 0.4f, 0.1f);
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
