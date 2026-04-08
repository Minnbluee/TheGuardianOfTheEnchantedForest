public class PlayerAttackState : IPlayerState
{
    private float _timer;
    private const float AttackDuration = 0.3f;

    public void Enter(PlayerController player)
    {
        UnityEngine.Debug.Log("[Player] Estado: Attack");
        _timer = 0f;
        player.ApplyHorizontalVelocity(0f);
    }

    public void Update(PlayerController player)
    {
        _timer += UnityEngine.Time.deltaTime;
        if (_timer >= AttackDuration)
            player.ChangeState(player.IdleState);
    }

    public void FixedUpdate(PlayerController player)
    {
        player.ApplyHorizontalVelocity(0f);
    }

    public void Exit(PlayerController player) { }
}