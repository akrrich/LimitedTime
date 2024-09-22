using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : IState
{
    private PlayerController playerController;

    public RunningState(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void Enter()
    {
        playerController.Anim.SetFloat("Movements", 1f);
        playerController.Speed = 7f;
    }

    public void Exit()
    {

    }

    public void UpdateState()
    {
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            playerController.StateController.TransitionTo(playerController.StateController.WalkingState);
        }

        if (Input.GetButtonDown("Jump") && playerController.IsGrounded)
        {
            playerController.IsGrounded = false;
            playerController.JumpForce = 7.5f;
            playerController.StateController.TransitionTo(playerController.StateController.JumpingState);
        }

        if (Input.GetButtonDown("Fire1") && playerController.CanShoot)
        {
            playerController.StateController.TransitionTo(playerController.StateController.ShootingState);
        }
    }
}
