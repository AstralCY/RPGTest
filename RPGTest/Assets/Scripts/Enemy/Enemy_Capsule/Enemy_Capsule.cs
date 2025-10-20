using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Capsule : Enemy
{
    public Enemy_CapsuleIdleState idleState { get; private set; }
    public Enemy_CapsuleMoveState moveState { get; private set; }
    public Enemy_CapsuleBattleState battleState { get; private set; }
    public Enemy_CapsuleAttackState attackState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        idleState = new Enemy_CapsuleIdleState(this, stateMachine, "Idle", this);
        moveState = new Enemy_CapsuleMoveState(this, stateMachine, "Idle", this);
        battleState = new Enemy_CapsuleBattleState(this, stateMachine, "Battle", this);
        attackState = new Enemy_CapsuleAttackState(this, stateMachine, "Attack", this);
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
