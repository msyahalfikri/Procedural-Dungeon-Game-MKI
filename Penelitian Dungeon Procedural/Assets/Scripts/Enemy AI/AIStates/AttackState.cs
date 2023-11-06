using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.AnimatedValues;

public class AttackState : AIState
{
    private bool leftHook = false, rightHook = false, heavyAttackReady = false;
    float attackInterval = 0.0f;
    float heavyAttackCooldown = 0.0f;
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

        heavyAttackCooldown -= Time.deltaTime;
        if (heavyAttackCooldown <= 0)
        {
            heavyAttackReady = true;
            heavyAttackCooldown = agent.config.heavyAttackCooldownMaxTime;
        }
        else
        {
            heavyAttackReady = false;
        }

        if (agent.alreadyAttacked)
        {
            // If already attacked, check if it's time to reset
            attackInterval -= Time.deltaTime;
            if (attackInterval <= 0.0f)
            {
                if (heavyAttackReady == true)
                {
                    agent.alreadyAttacked = false;
                    leftHook = false; rightHook = false;
                    agent.heavyAttack = true;
                }
                else if (heavyAttackReady == false)
                {
                    agent.heavyAttack = false;
                    agent.alreadyAttacked = false;
                    performLightAttack();
                    if (leftHook)
                    {
                        agent.attackLeft = true;
                        agent.attackRight = false;
                    }
                    else if (rightHook)
                    {
                        agent.attackLeft = false;
                        agent.attackRight = true;
                    }
                }

            }
            attackInterval = agent.config.timeBetweenAttacks;
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
        Debug.Log("LeftHook: " + agent.attackLeft + " || RightHook: " + agent.attackRight + " || HeavyAttackCooldown: " + heavyAttackCooldown);
    }
    private void performLightAttack()
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

