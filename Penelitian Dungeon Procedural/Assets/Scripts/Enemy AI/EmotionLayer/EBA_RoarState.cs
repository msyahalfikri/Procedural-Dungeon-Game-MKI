using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EBA_RoarState : AIState
{
    public float roarAnimationTimer;
    public AIStateID GetID()
    {
        return AIStateID.EBA_RoarState;
    }
    public void Enter(AIAgent agent)
    {
        agent.isRoaring = true;
        agent.navMeshAgent.isStopped = true;
        roarAnimationTimer = agent.config.roarAnimationMaxTime;
    }
    public void Update(AIAgent agent)
    {
        roarAnimationTimer -= Time.deltaTime;
        if (roarAnimationTimer <= 0.0f)
        {
            agent.isRoaring = false;
            if (!agent.isInAttackRange)
            {
                agent.stateMachine.ChangeState(AIStateID.EBA_ChasePlayer);
            }
            else
            {
                agent.stateMachine.ChangeState(AIStateID.EBA_AttackState);
            }
        }

    }
    public void Exit(AIAgent agent)
    {

    }
}
