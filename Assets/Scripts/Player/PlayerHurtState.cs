public class PlayerHurtState : IPlayerState
{
    private float _timer;
    private const float HurtDuration = 0.5f;

    public void Enter(PlayerController player)
    {
        UnityEngine.Debug.Log("[Player] Estado: Hurt");
        _timer = 0f;
        player.ApplyHorizontalVelocity(0f);
    }

    public void Update(PlayerController player)
    {
        _timer += UnityEngine.Time.deltaTime;
        if (_timer >= HurtDuration)
            player.ChangeState(player.IdleState);
    }

    public void FixedUpdate(PlayerController player) { }

    public void Exit(PlayerController player) { }
}