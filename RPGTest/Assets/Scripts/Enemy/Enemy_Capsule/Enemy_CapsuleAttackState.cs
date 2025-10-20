using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_CapsuleAttackState : EnemyAttackState
{
    Enemy_Capsule enemy_Capsule;

    public Enemy_CapsuleAttackState(Enemy _enemy, EnemyStateMachine _stateMachine, string _stateAnimName, Enemy_Capsule enemy_Capsule) : base(_enemy, _stateMachine, _stateAnimName)
    {
        this.enemy_Capsule = enemy_Capsule;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        enemy_Capsule.GetComponentInChildren<Enemy_CapsuleAttack>().InitAttack(enemy_Capsule);
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (canAttack == false)
        {
            stateMachine.ChangeState(enemy_Capsule.battleState);
        }
    }
}

