using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;

    private string stateAnimName;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _stateAnimName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.stateAnimName = _stateAnimName;
    }

    public virtual void OnEnter()
    {

    }

    public virtual void OnUpdate()
    {

    }

    public virtual void OnExit()
    {

    }
}
