using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EBA_IdleState : AIState
{
    public float waitTime;
    public AIStateID GetID()
    {
        return AIStateID.EBA_IdleState;
    }
    public void Enter(AIAgent agent)
    {
        agent.navMeshAgent.isStopped = true;
        agent.bodyIK.enabled = false;
        waitTime = Random.Range(agent.config.minIdleWaitTime, agent.config.maxIdleWaitTime);
    }
    public void Update(AIAgent agent)
    {
        if (agent.sightSensor.playerIsInSight && agent.isPlayerDead == false)
        {
            agent.stateMachine.ChangeState(AIStateID.EBA_RoarState);
        }

        waitTime -= Time.deltaTime;
        if (waitTime <= 0.0f)
        {
            agent.stateMachine.ChangeState(AIStateID.EBA_PatrolState);
        }
    }
    public void Exit(AIAgent agent)
    {

    }
}
