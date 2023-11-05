using UnityEngine;

public class PatrolState : AIState
{
    private bool walkPointSet = false;
    private Vector3 walkPoint;

    public AIStateID GetID()
    {
        return AIStateID.PatrolState;
    }

    public void Enter(AIAgent agent)
    {
        agent.navMeshAgent.isStopped = false;
        agent.bodyIK.enabled = false;
        walkPointSet = false;
        agent.navMeshAgent.speed = agent.config.agentWalkSpeed;
    }

    public void Update(AIAgent agent)
    {
        // Patrolling
        if (!walkPointSet)
        {
            // Search for a walk point
            float randomZ = Random.Range(-agent.config.walkPointRange, agent.config.walkPointRange);
            float randomX = Random.Range(-agent.config.walkPointRange, agent.config.walkPointRange);
            walkPoint = new Vector3(agent.transform.position.x + randomX, agent.transform.position.y, agent.transform.position.z + randomZ);

            if (Physics.Raycast(walkPoint, -Vector3.up, 2f, agent.GroundLayer))
            {
                walkPointSet = true;
            }
        }

        if (walkPointSet)
        {
            agent.navMeshAgent.SetDestination(walkPoint); // Use SetDestination to navigate to the walk point

            // Optionally, you can add a stopping distance check to ensure the AI doesn't stop too early
            if (Vector3.Distance(agent.transform.position, walkPoint) < agent.navMeshAgent.stoppingDistance)
            {
                agent.stateMachine.ChangeState(AIStateID.IdleState);
            }

        }

        if (agent.sightSensor.isPlayerInSight == true)
        {
            agent.stateMachine.ChangeState(AIStateID.ChasePlayer);
        }
    }

    public void Exit(AIAgent agent)
    {
    }
}
