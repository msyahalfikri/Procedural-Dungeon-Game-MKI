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
    public Transform playerTransform;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine = new AIStateMachine(this);

        //Register All States
        stateMachine.RegisterState(new ChaseState());
        stateMachine.RegisterState(new IdleState());

        //Initialize Initial State
        stateMachine.ChangeState(initialState);

        //Look For Player Game Object and It's Transform
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
    private void Update()
    {
        stateMachine.Update();
    }
}
