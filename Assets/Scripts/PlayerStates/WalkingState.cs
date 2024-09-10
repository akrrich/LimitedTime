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

    }

    public void Exit()
    {

    }

    public void UpdateState()
    {

    }
}
