using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : MonoBehaviour, IState
{
    private PlayerController playerController;

    public WalkingState(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void Enter()
    {
        playerController.Speed = 4;
    }

    public void Exit()
    {

    }

    public void UpdateState()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerController.StateController.TransitionTo(playerController.StateController.RunningState);
        }

        else if (Input.GetButtonDown("Jump") && playerController.IsGrounded)
        {
            playerController.IsGrounded = false;
            playerController.JumpForce = 5;
            playerController.StateController.TransitionTo(playerController.StateController.JumpingState);
        }

        else if (Mathf.Abs(playerController.Rb.velocity.x) < 0.1f && Mathf.Abs(playerController.Rb.velocity.z) < 0.1f)
        {
            playerController.StateController.TransitionTo(playerController.StateController.IdleState);
        }
    }
}
