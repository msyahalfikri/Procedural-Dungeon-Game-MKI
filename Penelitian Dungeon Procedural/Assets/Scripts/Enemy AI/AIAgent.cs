using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAgent : MonoBehaviour
{
    public AIStateMachine stateMachine;
    public AIStateID initialState;
    public NavMeshAgent navMeshAgent;
    public AiAgentConfig config;
    public GameObject playerTransform;
    [HideInInspector] public AIRagdoll ragdoll;
    [HideInInspector] public UIHealthBar healthBar;
    [HideInInspector] public BodyIK bodyIK;
    [HideInInspector] public SkinnedMeshRenderer mesh;
    [HideInInspector] public AISensor sightSensor;
    [HideInInspector] public ChaseRangeSphere chaseRangeSphere;
    [HideInInspector] public EnemyDamageDealer enemyDamageDealer;
    [HideInInspector] public Animator animator;
    [HideInInspector] public bool isDying;
    [HideInInspector] public bool attackLeft, attackRight, heavyAttack;
    [HideInInspector] public bool alreadyAttacked;
    [HideInInspector] public bool isAttacking;
    [HideInInspector] public bool isInAttackRange;
    [HideInInspector] public bool isInChaseRange;
    [HideInInspector] public bool turnedLeft, turnedRight, hasTurned;
    [HideInInspector] public bool isBlocking;
    [HideInInspector] public bool TakingDamage;
    [HideInInspector] public bool isRoaring;
    [HideInInspector] public bool isCheering;
    [HideInInspector] public bool isPlayerDead;



    public LayerMask PlayerLayer, GroundLayer;

    private void Awake()
    {
        isPlayerDead = false;
        ragdoll = GetComponentInChildren<AIRagdoll>();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        healthBar = GetComponentInChildren<UIHealthBar>();
        playerTransform = GameObject.FindGameObjectWithTag("Player");
        bodyIK = GetComponent<BodyIK>();
        sightSensor = GetComponent<AISensor>();
        chaseRangeSphere = GetComponentInChildren<ChaseRangeSphere>();
        animator = GetComponent<Animator>();
        enemyDamageDealer = GetComponentInChildren<EnemyDamageDealer>();
    }
    private void Start()
    {
        stateMachine = new AIStateMachine(this);

        //Register All States
        stateMachine.RegisterState(new TestingState());
        stateMachine.RegisterState(new ChaseState());
        stateMachine.RegisterState(new IdleState());
        stateMachine.RegisterState(new DeathState());
        stateMachine.RegisterState(new PatrolState());
        stateMachine.RegisterState(new AttackState());
        stateMachine.RegisterState(new BlockingState());
        stateMachine.RegisterState(new RoarState());
        stateMachine.RegisterState(new CheeringState());
        stateMachine.RegisterState(new FleeState());

        //Initialize Initial State
        stateMachine.ChangeState(initialState);
    }
    private void Update()
    {
        stateMachine.Update();
        Debug.Log(stateMachine.currentState);
    }
    public void DestroyThisEnemy()
    {
        Destroy(this.gameObject);
    }

    public void EnemyStartDealDamage()
    {
        enemyDamageDealer.EnemyStartDealDamage();
    }
    public void EnemyEndDealDamage()
    {
        enemyDamageDealer.EnemyEndDealDamage();
    }


    //Game event subscription
    void OnEnable()
    {
        GameEventHandler.onPlayerDeath += StopAttacking;
    }

    void OnDisable()
    {
        GameEventHandler.onPlayerDeath -= StopAttacking;
    }

    void StopAttacking(bool isPlayerDead)
    {
        stateMachine.ChangeState(AIStateID.IdleState);
        this.isPlayerDead = isPlayerDead;
    }
}
