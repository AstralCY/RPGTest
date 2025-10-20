using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleState : EnemyState
{
    protected bool IsAttack;
    protected bool IsBattle;
    protected bool IsBack;
    protected Player player;

    public EnemyBattleState(Enemy _enemy, EnemyStateMachine _stateMachine, string _stateAnimName) : base(_enemy, _stateMachine, _stateAnimName)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        IsBattle = true;
        IsAttack = false;
        IsBack = false;
        player = PlayerManager.Instance.player;
        enemy.enemyAgent.stoppingDistance = enemy.minRange;
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        float sqrdis = (enemy.transform.position - player.transform.position).sqrMagnitude;
        if (IsBack && enemy.enemyAgent.remainingDistance <= 0.1f)
        {
            IsBattle = false;
            IsAttack = false;
        }
        else if (IsBattle)
        {
            enemy.enemyAgent.SetDestination(player.transform.position);
            IsAttack = sqrdis <= enemy.minRange * enemy.minRange;
            if (sqrdis >= enemy.maxRange * enemy.maxRange)
            {
                enemy.enemyAgent.stoppingDistance = 0;
                enemy.enemyAgent.SetDestination(enemy.prevPos);
                IsBack = true;
                IsAttack = false;
            }
        }
    }
}
