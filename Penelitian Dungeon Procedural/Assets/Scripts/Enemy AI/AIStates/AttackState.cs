using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.AnimatedValues;

public class AttackState : AIState
{
    private bool leftHook = false, rightHook = false;
    float heavyAttackTimer;
    bool heavyAttackReady;
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
        heavyAttackTimer = agent.config.heavyAttackCooldownMaxTime;
    }

    public void Exit(AIAgent agent)
    {

    }

    public void Update(AIAgent agent)
    {
        heavyAttackTimer -= Time.deltaTime;
        if (heavyAttackTimer <= 0f)
        {
            heavyAttackReady = true;

        }
        else
        {
            heavyAttackReady = false;
        }

        if (agent.alreadyAttacked)
        {
            // If already attacked, check if it's time to reset
            agent.alreadyAttacked = false;
            attackInterval -= Time.deltaTime;
            if (attackInterval <= 0.0f)
            {
                if (heavyAttackReady)
                {
                    heavyAttackTimer = agent.config.heavyAttackCooldownMaxTime;
                    agent.attackLeft = false;
                    agent.attackRight = false;
                    agent.heavyAttack = true;
                }
                else
                {
                    PerformLightAttack();
                    if (leftHook)
                    {
                        agent.attackLeft = true;
                        agent.attackRight = false;
                        agent.heavyAttack = false;
                    }
                    else if (rightHook)
                    {
                        agent.attackLeft = false;
                        agent.attackRight = true;
                        agent.heavyAttack = false;
                    }
                }

            }
            attackInterval = agent.config.timeBetweenAttacks;
            Debug.Log("LeftHook: " + agent.attackLeft + " || RightHook: " + agent.attackRight + " || HeavyAttack: " + agent.heavyAttack);
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
        // Debug.Log("LeftHook: " + agent.attackLeft + " || RightHook: " + agent.attackRight + " || AttackTimer: " + attackInterval);
        // Debug.Log("HeavyAttack Timer: " + heavyAttackTimer + " || heavyAttackReady?: " + heavyAttackReady);
    }
    private void PerformLightAttack()
    {
        int randomAttack = UnityEngine.Random.Range(0, 2);
        if (randomAttack == 0)
        {
            leftHook = true;
            rightHook = false;
        }
        else
        {
            leftHook = false;
            rightHook = true;
        }
    }

}
