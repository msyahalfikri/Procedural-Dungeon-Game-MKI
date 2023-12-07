using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeState : AIState
{
    public float retreatThreshold = 30f; // Health threshold to trigger retreat
    public float retreatSpeed = 8f; // Speed when retreating
    public float safeDistance = 50f; // Safe distance from the player
    public Transform retreatPoint; // The point or position to retreat to

    private bool isRetreating = false;
    public AIStateID GetID()
    {
        return AIStateID.FleeState;
    }
    public void Enter(AIAgent agent)
    {

    }
    public void Update(AIAgent agent)
    {

    }


    public void Exit(AIAgent agent)
    {

    }



}
