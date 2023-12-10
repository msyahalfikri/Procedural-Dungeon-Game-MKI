using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.AnimatedValues;
using Unity.VisualScripting;

public class EBA_BlockingState : AIState
{
    float blockTimer = 0f;
    public AIStateID GetID()
    {
        return AIStateID.EBA_BlockingState;
    }
    public void Enter(AIAgent agent)
    {
        agent.isBlocking = true;
        blockTimer = agent.config.blockTimer;
    }

    public void Exit(AIAgent agent)
    {
        agent.isBlocking = false;
    }


    public void Update(AIAgent agent)
    {
        blockTimer -= Time.deltaTime;
        if (blockTimer <= 0f)
        {
            agent.stateMachine.ChangeState(AIStateID.EBA_AttackState);
        }

    }
}

