using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeState : AIState
{
    public float retreatSpeed = 6f; // Speed when retreating
    public float retreatDistance = 20f;
    private bool isRetreating = false;
    private Vector3 retreatDestination;
    public AIStateID GetID()
    {
        return AIStateID.FleeState;
    }
    public void Enter(AIAgent agent)
    {
        agent.navMeshAgent.isStopped = false;
        agent.navMeshAgent.speed = retreatSpeed;
        isRetreating = true;
    }
    public void Update(AIAgent agent)
    {
        if (isRetreating)
        {
            Vector3 direction = agent.transform.position - agent.playerTransform.transform.position;
            direction.Normalize();

            retreatDestination = agent.transform.position + direction * retreatDistance;
            agent.navMeshAgent.SetDestination(retreatDestination);
        }

        if (agent.navMeshAgent.remainingDistance <= agent.navMeshAgent.stoppingDistance)
        {
            isRetreating = false;
        }

        Debug.Log("IsRetrating: " + isRetreating + "|| RetreatDestination: " + retreatDestination);
    }


    public void Exit(AIAgent agent)
    {

    }

}
