public class PlayerCrouchState : IPlayerState
{
    public void Enter(PlayerController player)
    {
        UnityEngine.Debug.Log("[Player] Estado: Crouch");
    }

    public void Update(PlayerController player)
    {
        if (!player.CrouchHeld)
            player.ChangeState(player.IdleState);
    }

    public void FixedUpdate(PlayerController player)
    {
        player.ApplyHorizontalVelocity(0f);
    }

    public void Exit(PlayerController player) { }
}