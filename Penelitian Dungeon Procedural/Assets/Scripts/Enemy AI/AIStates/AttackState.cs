using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackState : AIState
{
    float attackInterval = 0.0f;
    public AIStateID GetID()
    {
        return AIStateID.AttackState;
    }
    public void Enter(AIAgent agent)
    {
        // agent.navMeshAgent.speed = 0.0f;
        agent.alreadyAttacked = false;
        attackInterval = agent.config.timeBetweenAttacks;
    }

    public void Exit(AIAgent agent)
    {

    }


    public void Update(AIAgent agent)
    {
        agent.navMeshAgent.transform.LookAt(agent.playerTransform.transform);
        if (agent.alreadyAttacked)
        {
            // If already attacked, check if it's time to reset
            attackInterval -= Time.deltaTime;
            if (attackInterval <= 0.0f)
            {
                agent.alreadyAttacked = false;
                attackInterval = agent.config.timeBetweenAttacks;
            }
        }
        else
        {
            // If not already attacked, check if it's time to attack
            attackInterval -= Time.deltaTime;
            if (attackInterval <= 0.0f)
            {
                agent.alreadyAttacked = true;
            }
        }
        Debug.Log(attackInterval);
    }
    private void PerformPunchAttack()
    {
        // Implement your punch attack logic here
        Debug.Log("Punch attack!");
    }

    private void PerformHeavyAttack()
    {
        // Implement your heavy attack logic here
        Debug.Log("Heavy attack!");
    }

    private void PerformJumpAttack()
    {
        // Implement your jump attack logic here
        Debug.Log("Jump attack!");
    }

}

