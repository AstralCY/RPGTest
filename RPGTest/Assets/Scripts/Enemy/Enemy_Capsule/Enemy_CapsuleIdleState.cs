using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_CapsuleIdleState : EnemyIdleState
{
    private Enemy_Capsule enemy_Capsule;

    public Enemy_CapsuleIdleState(Enemy _enemy, EnemyStateMachine _stateMachine, string _stateAnimName, Enemy_Capsule enemy_Capsule) : base(_enemy, _stateMachine, _stateAnimName)
    {
        this.enemy_Capsule = enemy_Capsule;
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (IsDetect == true)
        {
            stateMachine.ChangeState(enemy_Capsule.battleState);
        }
        else if (IsIdle == false)
        {
            stateMachine.ChangeState(enemy_Capsule.moveState);
        }
    }
}
