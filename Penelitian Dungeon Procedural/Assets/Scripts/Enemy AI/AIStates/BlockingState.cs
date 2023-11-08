using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.AnimatedValues;
using Unity.VisualScripting;

public class BlockingState : AIState
{
    bool isBlocking;
    bool walkingBackward = false;
    // Define a timer and a flag to track whether the AI should walk backward
    float walkBackwardTimer = 0f;

    // Set the duration of the delay before the AI starts moving backward
    float walkBackwardDelay = 0.6f; // You can adjust this value as needed
    public AIStateID GetID()
    {
        return AIStateID.BlockingState;
    }
    public void Enter(AIAgent agent)
    {
        isBlocking = true;
        agent.isBlocking = isBlocking;

    }

    public void Exit(AIAgent agent)
    {
        isBlocking = false;
        agent.isBlocking = isBlocking;
    }


    public void Update(AIAgent agent)
    {
        //=====Calculate Walking Backwards========
        Vector3 directionToPlayer = agent.playerTransform.transform.position - agent.transform.position;
        // Calculate the distance to the player
        float distanceToPlayer = directionToPlayer.magnitude;

        // Check if the player is walking towards the AI
        if (distanceToPlayer < agent.navMeshAgent.stoppingDistance)
        {
            walkBackwardTimer += Time.deltaTime;

            if (walkBackwardTimer >= walkBackwardDelay)
            {
                // If the timer runs out, move the AI backward and set walkingBackward to true
                Vector3 moveDirection = -directionToPlayer.normalized;
                agent.navMeshAgent.Move(moveDirection * agent.navMeshAgent.speed * Time.deltaTime);
                walkingBackward = true;
            }
        }
        else
        {
            // Reset the timer and stop the AI
            walkBackwardTimer = 0f;
            agent.navMeshAgent.velocity = Vector3.zero;
            walkingBackward = false;
        }

        // Set the walkingBackward boolean for the animation
        agent.IsWalkingBackward = walkingBackward;
    }
}

