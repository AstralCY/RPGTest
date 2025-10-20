using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_CapsuleBattleState : EnemyBattleState
{
    private Enemy_Capsule enemy_Capsule;

    public Enemy_CapsuleBattleState(Enemy _enemy, EnemyStateMachine _stateMachine, string _stateAnimName, Enemy_Capsule enemy_Capsule) : base(_enemy, _stateMachine, _stateAnimName)
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
        if (IsAttack)
        {
            stateMachine.ChangeState(enemy_Capsule.attackState);
        }
        else if (IsBattle == false)
        {
            stateMachine.ChangeState(enemy_Capsule.moveState);
        }
    }
}
