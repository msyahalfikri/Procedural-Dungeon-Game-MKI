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
    [HideInInspector] public GameObject playerTransform;
    [HideInInspector] public AIRagdoll ragdoll;
    [HideInInspector] public UIHealthBar healthBar;
    [HideInInspector] public BodyIK bodyIK;
    [HideInInspector] public SkinnedMeshRenderer mesh;
    [HideInInspector] public AISensor sightSensor;
    [HideInInspector] public ChaseRangeSphere chaseRangeSphere;
    public LayerMask PlayerLayer, GroundLayer;

    private void Awake()
    {
        ragdoll = GetComponent<AIRagdoll>();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        healthBar = GetComponentInChildren<UIHealthBar>();
        playerTransform = GameObject.FindGameObjectWithTag("Player");
        bodyIK = GetComponent<BodyIK>();
        sightSensor = GetComponent<AISensor>();
        chaseRangeSphere = GetComponentInChildren<ChaseRangeSphere>();
    }
    private void Start()
    {
        stateMachine = new AIStateMachine(this);

        //Register All States
        stateMachine.RegisterState(new ChaseState());
        stateMachine.RegisterState(new IdleState());
        stateMachine.RegisterState(new DeathState());
        stateMachine.RegisterState(new PatrolState());

        //Initialize Initial State
        stateMachine.ChangeState(initialState);
    }
    private void Update()
    {
        stateMachine.Update();
        Debug.Log(stateMachine.currentState);
    }


}
