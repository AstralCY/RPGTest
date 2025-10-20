using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{

    [HideInInspector] public NavMeshAgent playerAgent;
    [HideInInspector] public PlayerStatus status;
    [HideInInspector] public Rigidbody rb;

    public Weapon currentWeapon;


    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerAttackState attackState { get; private set; }

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        attackState = new PlayerAttackState(this, stateMachine, "Attack");
    }

    private void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();
        stateMachine.Init(idleState);
        rb = GetComponent<Rigidbody>();

        status = GetComponent<PlayerStatus>();
        if (currentWeapon == null) {
           var itemdata = Inventory.Instance.GetItem(2);
            var prefab = itemdata.itemPrefab;
            prefab.GetComponentInChildren<IEquip>().Arm();
        }//测试默认装备了武器代码
        else
        {
            status.AddAtk(currentWeapon.GetValue(ItemData.ItemPropertyType.ATK));
        }
    }

    private void Update()
    {
        stateMachine.UpdateState();
    }

    public void StartPlayerCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }

    public void StopPlayerCoroutine(IEnumerator coroutine)
    {
        StopCoroutine(coroutine);
    }
}