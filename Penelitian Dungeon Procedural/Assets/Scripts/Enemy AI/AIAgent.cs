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
    [HideInInspector] public bool isDying;
    [HideInInspector] public bool attackLeft, attackRight, heavyAttack;
    [HideInInspector] public bool alreadyAttacked;
    [HideInInspector] public bool IsInAttackRange;
    [HideInInspector] public bool turnedLeft, turnedRight, hasTurned;
    [HideInInspector] public bool IsWalkingBackward;
    [HideInInspector] public bool isBlocking;
    [HideInInspector] public Animator animator;
    public LayerMask PlayerLayer, GroundLayer;

    private void Awake()
    {
        ragdoll = GetComponentInChildren<AIRagdoll>();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        healthBar = GetComponentInChildren<UIHealthBar>();
        playerTransform = GameObject.FindGameObjectWithTag("Player");
        bodyIK = GetComponent<BodyIK>();
        sightSensor = GetComponent<AISensor>();
        chaseRangeSphere = GetComponentInChildren<ChaseRangeSphere>();
        animator = GetComponent<Animator>();
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

        //Initialize Initial State
        stateMachine.ChangeState(initialState);
    }
    private void Update()
    {
        stateMachine.Update();
        // Debug.Log(stateMachine.currentState);
    }


}
