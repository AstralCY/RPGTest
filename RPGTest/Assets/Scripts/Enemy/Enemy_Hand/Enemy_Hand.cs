using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Hand : Enemy
{
    public Enemy_HandIdleState idleState { get; private set; }
    public Enemy_HandMoveState moveState { get; private set; }
    public Enemy_HandBattleState battleState { get; private set; }
    public Enemy_HandAttackState attackState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        idleState = new Enemy_HandIdleState(this, stateMachine, "Idle", this);
        moveState = new Enemy_HandMoveState(this, stateMachine, "Idle", this);
        battleState = new Enemy_HandBattleState(this, stateMachine, "Battle", this);
        attackState = new Enemy_HandAttackState(this, stateMachine, "Attack", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Init(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }
}
