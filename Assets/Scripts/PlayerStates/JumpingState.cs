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

    }

    public void Exit()
    {

    }

    public void UpdateState() 
    { 

    }
}
