using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class ShootingState : IState
{
    private PlayerController playerController;


    public ShootingState(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public void Enter()
    {
        //playerController.Anim.SetFloat("Movements", 2f);
        playerController.BulletPool.CounterBullets--;

        Bu bullet = playerController.BulletPool.GetBullet();
        bullet.InstantiateBullet(playerController.CameraTransform.transform, playerController.BulletPool);
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
    }
}
