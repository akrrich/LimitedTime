using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : MonoBehaviour, IState
{
    private PlayerController playerController;


    public RunningState(PlayerController playerController)
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
