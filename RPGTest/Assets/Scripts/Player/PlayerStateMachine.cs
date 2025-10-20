using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    private PlayerState currentState;

    public void Init(PlayerState _startState)
    {
        currentState = _startState;
        currentState.OnEnter();
    }

    public void ChangeState(PlayerState _newState)
    {
        currentState.OnExit();
        currentState = _newState;
        currentState.OnEnter();
    }

    public void UpdateState()
    {
        currentState.OnUpdate();
    }

    public PlayerState GetCurrentState()
    {
        return currentState;
    }
}
