using UnityEngine;

public class EnemyMoveState : EnemyState
{
    protected bool IsMoving;
    protected bool IsDetect;
    public EnemyMoveState(Enemy _enemy, EnemyStateMachine _stateMachine, string _stateAnimName) : base(_enemy, _stateMachine, _stateAnimName)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        IsMoving = true;
        IsDetect = false;
        if (enemy.enemyAgent != null)
        {
            enemy.enemyAgent.SetDestination(GetRandomPos());
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (enemy.DetectPlayer())
        {
            IsDetect = true;
        }
        else if (enemy.enemyAgent.remainingDistance <= 0.1f)
        {
            IsMoving = false;
        }
    }

    private Vector3 GetRandomPos()
    {
        Vector3 randonDir = new Vector3(Random.Range(-1, 1f), 0, Random.Range(-1, 1f));
        return enemy.transform.position + randonDir.normalized * Random.Range(3, 7);
    }
}
