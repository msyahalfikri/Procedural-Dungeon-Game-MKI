using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBA_FleeState : AIState
{
    private bool isRetreating = false;
    private Vector3 retreatDestination;
    public AIStateID GetID()
    {
        return AIStateID.EBA_FleeState;
    }
    public void Enter(AIAgent agent)
    {
        agent.navMeshAgent.isStopped = false;
        agent.navMeshAgent.speed = agent.config.retreatSpeed;
        isRetreating = true;
        agent.bodyIK.enabled = false;
    }
    public void Update(AIAgent agent)
    {
        if (isRetreating)
        {
            Vector3 direction = agent.transform.position - agent.playerTransform.transform.position;
            direction.Normalize();

            retreatDestination = agent.transform.position + direction * agent.config.retreatDistance;
            agent.navMeshAgent.SetDestination(retreatDestination);
        }

        if (agent.navMeshAgent.remainingDistance <= agent.navMeshAgent.stoppingDistance)
        {
            isRetreating = false;
        }

        // Debug.Log("IsRetrating: " + isRetreating + "|| RetreatDestination: " + retreatDestination);
    }


    public void Exit(AIAgent agent)
    {

    }

}
