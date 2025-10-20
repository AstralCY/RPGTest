using UnityEngine;

public class EnemyAttackState : EnemyState
{
    protected bool canAttack;
    Player player;

    public EnemyAttackState(Enemy _enemy, EnemyStateMachine _stateMachine, string _stateAnimName) : base(_enemy, _stateMachine, _stateAnimName)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        canAttack = true;
        player = PlayerManager.Instance.player;
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        float dis = Vector3.Distance(enemy.transform.position, player.transform.position);
        if (dis > enemy.minRange)
        {
            canAttack = false;
        }
    }
}
