using UnityEngine;

public class PlayerIdleState : IPlayerState
{
    public void Enter(PlayerController player)
    {
        Debug.Log("[Player] Estado: Idle");
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
        if (Mathf.Abs(player.MoveInput) > 0.01f)
            player.ChangeState(player.MoveState);
    }

    public void FixedUpdate(PlayerController player)
    {
        player.ApplyHorizontalVelocity(0f);
    }

    public void Exit(PlayerController player) { }
}