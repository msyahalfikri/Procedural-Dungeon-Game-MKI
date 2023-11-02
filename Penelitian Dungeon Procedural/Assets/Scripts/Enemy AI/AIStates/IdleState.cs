using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class IdleState : AIState
{
    public float waitTime;
    public AIStateID GetID()
    {
        return AIStateID.IdleState;
    }
    public void Enter(AIAgent agent)
    {
        agent.navMeshAgent.isStopped = true;
        agent.bodyIK.enabled = false;
        waitTime = agent.config.IdleWaitTime;
    }
    public void Update(AIAgent agent)
    {
        if (agent.sightSensor.isPlayerInSight == true)
        {
            agent.stateMachine.ChangeState(AIStateID.ChasePlayer);
        }

        waitTime -= Time.deltaTime;
        if (waitTime <= 0.0f)
        {
            agent.stateMachine.ChangeState(AIStateID.PatrolState);
        }
    }
    public void Exit(AIAgent agent)
    {

    }
}
