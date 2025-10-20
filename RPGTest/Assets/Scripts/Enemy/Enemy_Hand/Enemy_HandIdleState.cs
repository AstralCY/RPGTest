using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_HandIdleState : EnemyIdleState
{
    private Enemy_Hand enemy_Hand;

    public Enemy_HandIdleState(Enemy _enemy, EnemyStateMachine _stateMachine, string _stateAnimName, Enemy_Hand enemy_Hand) : base(_enemy, _stateMachine, _stateAnimName)
    {
        this.enemy_Hand = enemy_Hand;
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
        if (IsDetect)
        {
            stateMachine.ChangeState(enemy_Hand.battleState);
        }
        else if (!IsIdle)
        {
            stateMachine.ChangeState(enemy_Hand.moveState);
        }
    }
}
