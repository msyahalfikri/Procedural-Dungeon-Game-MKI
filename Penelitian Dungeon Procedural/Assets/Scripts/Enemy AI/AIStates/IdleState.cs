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
        if (agent.sightSensor.isPlayerInSight == true)
        {
            agent.stateMachine.ChangeState(AIStateID.ChasePlayer);
        }
    }
    public void Exit(AIAgent agent)
    {

    }
}
