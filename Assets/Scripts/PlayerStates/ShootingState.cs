using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShootinState : IState
{
    private PlayerController playerController;
    
    public ShootinState(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void Enter()
    {
        Bullet bullet = playerController.BulletPool.GetBullet();
        bullet.InstantiateBullet(playerController.CameraTransform, playerController.BulletPool);
    }

    public void Exit()
    {

    }

    public void UpdateState()
    {
        if (Mathf.Abs(playerController.Rb.velocity.x) < 0.1f && Mathf.Abs(playerController.Rb.velocity.z) < 0.1f)
        {
            playerController.StateController.TransitionTo(playerController.StateController.IdleState);
        }

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            playerController.StateController.TransitionTo(playerController.StateController.WalkingState);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerController.StateController.TransitionTo(playerController.StateController.RunningState);
        }

        if (Input.GetButtonDown("Jump") && playerController.IsGrounded)
        {
            playerController.IsGrounded = false;
            playerController.JumpForce = 7.5f;
            playerController.StateController.TransitionTo(playerController.StateController.JumpingState);
        }
    }
}
