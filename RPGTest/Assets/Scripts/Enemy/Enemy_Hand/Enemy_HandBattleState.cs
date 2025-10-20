using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_HandBattleState : EnemyBattleState
{
    private Enemy_Hand enemy_Hand;

    public Enemy_HandBattleState(Enemy _enemy, EnemyStateMachine _stateMachine, string _stateAnimName, Enemy_Hand enemy_Hand) : base(_enemy, _stateMachine, _stateAnimName)
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
        if (IsAttack)
        {
            stateMachine.ChangeState(enemy_Hand.attackState);
        }
        else if (!IsBattle)
        {
            stateMachine.ChangeState(enemy_Hand.moveState);
        }
    }
}
