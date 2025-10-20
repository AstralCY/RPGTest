using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_HandAttackState : EnemyAttackState
{
    private Enemy_Hand enemy_Hand;

    public Enemy_HandAttackState(Enemy _enemy, EnemyStateMachine _stateMachine, string _stateAnimName, Enemy_Hand enemy_Hand) : base(_enemy, _stateMachine, _stateAnimName)
    {
        this.enemy_Hand = enemy_Hand;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        enemy_Hand.GetComponentInChildren<Enemy_HandAttack>().InitAttack(enemy_Hand);
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (!canAttack)
        {
            stateMachine.ChangeState(enemy_Hand.battleState);
        }
    }
}
