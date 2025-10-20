using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_HandMoveState : EnemyMoveState
{
    private Enemy_Hand enemy_Hand;

    public Enemy_HandMoveState(Enemy _enemy, EnemyStateMachine _stateMachine, string _stateAnimName, Enemy_Hand enemy_Hand) : base(_enemy, _stateMachine, _stateAnimName)
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
        else if (!IsMoving)
        {
            stateMachine.ChangeState(enemy_Hand.idleState);
        }
    }
}
