using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    protected bool IsIdle;
    protected bool IsDetect;

    public EnemyIdleState(Enemy _enemy, EnemyStateMachine _stateMachine, string _stateAnimName) : base(_enemy, _stateMachine, _stateAnimName)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        timer = enemy.idletime;
        IsIdle = true;
        IsDetect = false;
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        timer -= Time.deltaTime;
        if (enemy.DetectPlayer())
        {
            IsDetect = true;
        }
        else if (timer < 0)
        {
            IsIdle = false;
        }
    }
}
