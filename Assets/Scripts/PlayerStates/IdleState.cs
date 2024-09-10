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

    }
}
