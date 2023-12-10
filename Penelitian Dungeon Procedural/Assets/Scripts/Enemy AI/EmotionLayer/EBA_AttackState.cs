using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.AnimatedValues;

public class EBA_AttackState : AIState
{
    private bool leftHook = false, rightHook = false, blocking = false;
    private bool heavyAttackReady;
    private float heavyAttackTimer;
    private float attackIntervalTimer = 0.0f;
    public AIStateID GetID()
    {
        return AIStateID.EBA_AttackState;
    }
    public void Enter(AIAgent agent)
    {
        agent.alreadyAttacked = false;
        attackIntervalTimer = agent.config.timeBetweenAttacks;
        heavyAttackTimer = agent.config.heavyAttackCooldownMaxTime;
        agent.bodyIK.enabled = true;
    }

    public void Exit(AIAgent agent)
    {

    }

    public void Update(AIAgent agent)
    {
        UpdateHeavyAttackTimer();

        if (agent.alreadyAttacked)
        {
            ResetAttackIfNeeded(agent);
        }
        else
        {
            CheckTimeToAttack(agent);
        }
        // Debug.Log("LeftHook: " + agent.attackLeft + " || RightHook: " + agent.attackRight + " || AttackTimer: " + attackInterval);
        // Debug.Log("HeavyAttack Timer: " + heavyAttackTimer + " || heavyAttackReady?: " + heavyAttackReady);
    }

    //AI Performs light Attack. Has a chance to attack with left or right hook
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

    //AI Decides between performing a light Attack or a CHANCE to block
    private void PerformAction(float blockChanceInPercent)
    {
        int blockChance = UnityEngine.Random.Range(1, 101);
        if (blockChance < blockChanceInPercent)
        {
            leftHook = false;
            rightHook = false;
            blocking = true;
        }
        else
        {
            PerformLightAttack();
            blocking = false;
        }
    }


    //Reset the AlreadyAttack flag to prepare for attacking again. Can either perform a Heavy Attack or an action between light attack or block
    private void ResetAttackIfNeeded(AIAgent agent)
    {
        agent.alreadyAttacked = false;
        attackIntervalTimer -= Time.deltaTime;

        if (attackIntervalTimer <= 0.0f)
        {
            if (heavyAttackReady)
            {
                PrepareHeavyAttack(agent);
            }
            else
            {
                ChooseAttackType(agent);
            }
            attackIntervalTimer = agent.config.timeBetweenAttacks;
        }
    }

    //AI performs heavy attack
    private void PrepareHeavyAttack(AIAgent agent)
    {
        heavyAttackTimer = agent.config.heavyAttackCooldownMaxTime;
        agent.attackLeft = false;
        agent.attackRight = false;
        agent.heavyAttack = true;
    }

    //AI decides to perform between [left hook, right hook, or block]
    private void ChooseAttackType(AIAgent agent)
    {
        PerformAction(agent.config.blockChanceInPercent);

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
        else if (blocking)
        {
            agent.attackLeft = false;
            agent.attackRight = false;
            agent.heavyAttack = false;
            agent.stateMachine.ChangeState(AIStateID.EBA_BlockingState);
        }
    }

    //Update the cooldown of heavy attack
    private void UpdateHeavyAttackTimer()
    {
        heavyAttackTimer -= Time.deltaTime;
        heavyAttackReady = heavyAttackTimer <= 0f;
    }
    //Check whether the AI can attack or not
    private void CheckTimeToAttack(AIAgent agent)
    {
        attackIntervalTimer -= Time.deltaTime;

        if (attackIntervalTimer <= 0.0f)
        {
            agent.alreadyAttacked = true;
        }
    }
}

