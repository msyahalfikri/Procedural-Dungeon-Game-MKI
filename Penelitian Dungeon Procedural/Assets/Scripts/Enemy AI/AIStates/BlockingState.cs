using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.AnimatedValues;
using Unity.VisualScripting;

public class BlockingState : AIState
{
    bool isBlocking;
    float blockTimer = 0f;// You can adjust this value as needed
    public AIStateID GetID()
    {
        return AIStateID.BlockingState;
    }
    public void Enter(AIAgent agent)
    {
        isBlocking = true;
        agent.isBlocking = isBlocking;
        blockTimer = agent.config.blockTimer;
    }

    public void Exit(AIAgent agent)
    {
        isBlocking = false;
        agent.isBlocking = isBlocking;
    }


    public void Update(AIAgent agent)
    {
        // blockTimer -= Time.deltaTime;
        if (blockTimer <= 0f)
        {
            agent.stateMachine.ChangeState(AIStateID.AttackState);
            // Vector3(-1.85716544e-05,-0.000559724867,5.86504211e-06)
            // Vector3(16.6946545,19.4099503,10.260107)
        }
    }
}

