using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.AnimatedValues;
using Unity.VisualScripting;

public class HeavyAttackState : AIState
{
    // private bool heavyAttackReady = false;
    // float heavyAttackCooldown = 0.0f;
    public AIStateID GetID()
    {
        return AIStateID.HeavyAttackState;
    }
    public void Enter(AIAgent agent)
    {
        // // agent.navMeshAgent.speed = 0.0f;
        // agent.heavyAttack = false;
        // heavyAttackCooldown = agent.config.heavyAttackCooldownMaxTime;
    }

    public void Exit(AIAgent agent)
    {

    }


    public void Update(AIAgent agent)
    {
        // agent.navMeshAgent.transform.LookAt(agent.playerTransform.transform);

        // heavyAttackCooldown -= Time.deltaTime;
        // if (heavyAttackCooldown <= 0)
        // {
        //     heavyAttackReady = true;
        //     agent.stateMachine.ChangeState(AIStateID.AttackState);
        // }
        // else
        // {
        //     heavyAttackReady = false;
        // }
        // agent.heavyAttack = heavyAttackReady;
        // Debug.Log("HeavyAttackCooldown: " + heavyAttackCooldown + " || Is it Ready? " + heavyAttackReady);
        // Calculate the direction to the player


    }
}

