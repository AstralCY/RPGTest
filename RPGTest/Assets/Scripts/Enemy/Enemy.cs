using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public EnemyStateMachine stateMachine { get; private set; }

    public NavMeshAgent enemyAgent;
    public Animator anim;
    public CharacterStatus status;
    public LayerMask CheckPlayer;

    [Header("StateInfo")]
    public float idletime;
    public float maxRange = 10f;
    public float minRange = 1f;

    [HideInInspector] public Vector3 prevPos;

    protected virtual void Awake()
    {
        stateMachine = new EnemyStateMachine();
        enemyAgent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        status = GetComponent<EnemyStatus>();
    }

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
        stateMachine.UpdateState();
    }

    public virtual bool DetectPlayer()
    {
        Collider[] colliders = new Collider[10];
        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, maxRange, colliders, CheckPlayer);
        for (int i = 0; i < hitCount; i++)
        {
            Vector3 dir = colliders[i].transform.position - transform.position;
            float angle = Vector3.Angle(transform.forward, dir);
            if (angle < 60f)
            {
                prevPos = transform.position;
                return true;
            }
        }
        return false;
    }
}
