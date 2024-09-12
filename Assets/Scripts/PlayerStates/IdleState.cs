using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : MonoBehaviour, IState
{
    private PlayerController playerController;

    public IdleState(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void Enter()
    {

    }

    public void Exit()
    {

    }

    public void UpdateState()
    {
        if (Mathf.Abs(playerController.Rb.velocity.x) > 0.1f || Mathf.Abs(playerController.Rb.velocity.y) > 0.1f)
        {
            playerController.StateController.TransitionTo(playerController.StateController.WalkingState);
        }

        else if (Input.GetButtonDown("Jump") && playerController.IsGrounded)
        {
            playerController.IsGrounded = false;
            playerController.JumpForce = 2.5f;
            playerController.StateController.TransitionTo(playerController.StateController.JumpingState);
        }
    }
}
