using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    private EnemyState currentState;

    public void Init(EnemyState _startState)
    {
        currentState = _startState;
        currentState.OnEnter();
    }

    public void ChangeState(EnemyState _newState)
    {
        currentState.OnExit();
        currentState = _newState;
        currentState.OnEnter();
    }

    public void UpdateState()
    {
        currentState.OnUpdate();
    }
}
