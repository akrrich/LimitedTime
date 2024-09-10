using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    private IState currentState;

    public IState CurrentState { get { return currentState; } }

    private IdleState idleState;
    private WalkingState walkingState;
    private RunningState runningState;
    private JumpingState jumpingState;

    public IdleState IdleState { get { return idleState; } }
    public WalkingState WalkingState { get { return walkingState; } }
    public RunningState RunnignState { get { return runningState; } }
    public JumpingState JumpingState { get { return jumpingState; } }


    public StateController(PlayerController playerController)
    {
        idleState = new IdleState(playerController);
        walkingState = new WalkingState(playerController); 
        runningState = new RunningState(playerController); 
        jumpingState = new JumpingState(playerController);
    }

    public void InitializeState(IState state)
    {
        currentState = state;
        state.Enter();
    }

    public void TransitionTo(IState state)
    {
        currentState.Exit();
        currentState = state;
        currentState.Enter();
    }

    public void UpdateState()
    {
        currentState.UpdateState();
    }
}
