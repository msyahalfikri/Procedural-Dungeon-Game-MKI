using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSphereCollider : MonoBehaviour
{
    AIAgent agent;

    private void Start()
    {
        agent = GetComponentInParent<AIAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            agent.isInAttackRange = true;
            if (agent.stateMachine.currentState == AIStateID.ChasePlayer)
            {
                Debug.Log("Player in Attack Range");
                agent.stateMachine.ChangeState(AIStateID.AttackState);
            }

        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            agent.isInAttackRange = false;
            if (agent.stateMachine.currentState == AIStateID.AttackState || agent.stateMachine.currentState == AIStateID.BlockingState)
            {
                Debug.Log("Player not in Attack Range");
                agent.stateMachine.ChangeState(AIStateID.ChasePlayer);
            }

        }

    }
}
