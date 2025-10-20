using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine stateMachine;
    protected Enemy enemy;

    protected float timer;

    private string stateAnimName;

    public EnemyState(Enemy _enemy, EnemyStateMachine _stateMachine, string _stateAnimName)
    {
        this.enemy = _enemy;
        this.stateMachine = _stateMachine;
        this.stateAnimName = _stateAnimName;
    }

    public virtual void OnEnter()
    {
        enemy.anim.SetBool(stateAnimName, true);
    }

    public virtual void OnExit()
    {
        enemy.anim.SetBool(stateAnimName, false);
    }

    public virtual void OnUpdate()
    {
        
    }
}
