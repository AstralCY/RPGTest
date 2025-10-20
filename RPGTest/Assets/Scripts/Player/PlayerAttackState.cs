using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(Player _player, PlayerStateMachine _stateMachine, string _stateAnimName) : base(_player, _stateMachine, _stateAnimName)
    {
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
        if (player.currentWeapon != null)
        {
            player.currentWeapon.Attack();
        }
        stateMachine.ChangeState(player.idleState);
    }
}
