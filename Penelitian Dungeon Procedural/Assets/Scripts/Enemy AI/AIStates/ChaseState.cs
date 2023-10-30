using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : AIState
{

    float chaseTimer = 0.0f;
    public AIStateID GetID()
    {
        return AIStateID.ChasePlayer;
    }
    public void Enter(AIAgent agent)
    {
        agent.navMeshAgent.isStopped = false;
    }
    public void Update(AIAgent agent)
    {
        if (!agent.enabled)
        {
            return;
        }
        chaseTimer -= Time.deltaTime;
        if (!agent.navMeshAgent.hasPath)
        {
            agent.navMeshAgent.destination = agent.playerTransform.transform.position;
        }
        if (chaseTimer < 0.0f)
        {
            Vector3 direction = (agent.playerTransform.transform.position - agent.navMeshAgent.destination);
            direction.y = 0;
            if (direction.sqrMagnitude > agent.config.maxDistance * agent.config.maxDistance)
            {
                if (agent.navMeshAgent.pathStatus != NavMeshPathStatus.PathPartial)
                {
                    agent.navMeshAgent.destination = agent.playerTransform.transform.position;
                }
            }
            chaseTimer = agent.config.maxTime;
        }


    }
    public void Exit(AIAgent agent)
    {

    }

}
