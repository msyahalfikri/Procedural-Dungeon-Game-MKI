using UnityEngine;
using System.Collections.Generic;

public class PatrolState : AIState
{
    bool walkPointSet = false;
    private Vector3 walkPoint;

    public AIStateID GetID()
    {
        return AIStateID.PatrolState;
    }

    public void Enter(AIAgent agent)
    {
        agent.enabled = true;
        agent.navMeshAgent.isStopped = false;
        agent.bodyIK.enabled = false;
        walkPointSet = false;
    }

    public void Update(AIAgent agent)
    {
        //Patrolling
        if (!walkPointSet)
        {
            //Search for WalkPoint
            float randomZ = Random.Range(-agent.config.walkPointRange, agent.config.walkPointRange);
            float randomX = Random.Range(-agent.config.walkPointRange, agent.config.walkPointRange);
            walkPoint = new Vector3(agent.transform.position.x + randomX, agent.transform.position.y, agent.transform.position.z + randomZ);

            if (Physics.Raycast(walkPoint, -agent.transform.up, 2f, agent.GroundLayer))
            {
                walkPointSet = true;

            }

        }

        if (walkPointSet) agent.navMeshAgent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = agent.transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;
    }

    public void Exit(AIAgent agent)
    {
    }
}
