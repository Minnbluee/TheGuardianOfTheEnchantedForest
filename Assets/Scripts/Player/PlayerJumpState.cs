using UnityEngine;

public class PlayerJumpState : IPlayerState
{
    public void Enter(PlayerController player)
    {
        Debug.Log("[Player] Estado: Jump");
        player.ApplyJumpForce();
    }

    public void Update(PlayerController player)
    {
        if (player.VerticalVelocity < -0.1f)
        {
            player.ChangeState(player.FallState);
            return;
        }
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