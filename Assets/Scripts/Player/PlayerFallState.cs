using UnityEngine;

public class PlayerFallState : IPlayerState
{
    public void Enter(PlayerController player)
    {
        Debug.Log("[Player] Estado: Fall");
    }

    public void Update(PlayerController player)
    {
        if (player.IsGrounded && player.VerticalVelocity <= 0)
        {
            player.ChangeState(Mathf.Abs(player.MoveInput) > 0.01f
                ? (IPlayerState)player.MoveState
                : player.IdleState);
        }
    }

    public void FixedUpdate(PlayerController player)
    {
        player.ApplyHorizontalVelocity(player.MoveInput);
        player.HandleFlip();
    }

    public void Exit(PlayerController player) { }
}