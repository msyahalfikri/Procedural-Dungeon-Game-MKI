using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : AIState
{
    public AIStateID GetID()
    {
        return AIStateID.IdleState;
    }
    public void Enter(AIAgent agent)
    {
        agent.navMeshAgent.isStopped = true;
    }
    public void Update(AIAgent agent)
    {
        Vector3 playerDirection = agent.playerTransform.position - agent.transform.position;
        if (playerDirection.magnitude > agent.config.maxSightDistance)
        {
            return;
        }

        Vector3 agentDirection = agent.transform.forward;
        playerDirection.Normalize();
        float dotProduct = Vector3.Dot(playerDirection, agentDirection);
        if (dotProduct > 0.0f)
        {
            //change to chaseplayer state
        }
    }
    public void Exit(AIAgent agent)
    {

    }
}
