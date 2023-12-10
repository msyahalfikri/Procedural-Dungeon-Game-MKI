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
            else if (agent.stateMachine.currentState == AIStateID.EBA_ChasePlayer)
            {
                agent.stateMachine.ChangeState(AIStateID.EBA_AttackState);
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
                agent.stateMachine.ChangeState(AIStateID.ChasePlayer);
            }
            else if (agent.stateMachine.currentState == AIStateID.EBA_AttackState || agent.stateMachine.currentState == AIStateID.EBA_BlockingState)
            {
                agent.stateMachine.ChangeState(AIStateID.EBA_ChasePlayer);
            }
            Debug.Log("Player not in Attack Range");
        }

    }
}
