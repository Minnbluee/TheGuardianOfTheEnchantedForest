using UnityEngine;

public class PlayerMoveState : IPlayerState
{
    public void Enter(PlayerController player)
    {
        Debug.Log("[Player] Estado: Move");
    }

    public void Update(PlayerController player)
    {
        if (player.CrouchHeld)
        {
            player.ChangeState(player.CrouchState);
            return;
        }
        if (player.JumpPressed)
        {
            player.ChangeState(player.JumpState);
            return;
        }
        if (player.AttackPressed)
        {
            player.ChangeState(player.AttackState);
            return;
        }
        if (Mathf.Abs(player.MoveInput) <= 0.01f)
            player.ChangeState(player.IdleState);
    }

    public void FixedUpdate(PlayerController player)
    {
        player.ApplyHorizontalVelocity(player.MoveInput);
        player.HandleFlip();
    }

    public void Exit(PlayerController player) { }
}