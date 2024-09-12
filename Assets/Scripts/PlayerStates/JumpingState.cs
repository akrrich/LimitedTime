using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : MonoBehaviour, IState
{
    private PlayerController playerController;

    public JumpingState(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void Enter()
    {
        playerController.Rb.AddForce(Vector3.up * playerController.JumpForce, ForceMode.Impulse);
    }

    public void Exit()
    {
        playerController.JumpForce = 0;
    }

    public void UpdateState() 
    {
        if (playerController.IsGrounded)
        {
            playerController.StateController.TransitionTo(playerController.StateController.IdleState);
        }
    }
}
