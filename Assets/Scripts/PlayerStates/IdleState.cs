using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private PlayerController playerController;

    public IdleState(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void Enter()
    {
        playerController.Anim.SetFloat("Movements", 0f);
    }

    public void Exit()
    {
        
    }

    public void UpdateState()
    {
        if (Mathf.Abs(playerController.Rb.velocity.x) > 0.1f || Mathf.Abs(playerController.Rb.velocity.z) > 0.1f)
        {
            playerController.StateController.TransitionTo(playerController.StateController.WalkingState);
        }

        if (Input.GetButtonDown("Jump") && playerController.IsGrounded)
        {
            playerController.IsGrounded = false;
            playerController.JumpForce = 2.5f;
            playerController.StateController.TransitionTo(playerController.StateController.JumpingState);
        }

        if (Input.GetButtonDown("Fire1") && playerController.CanShoot)
        {
            playerController.StateController.TransitionTo(playerController.StateController.ShootingState);
        }
    }
}
