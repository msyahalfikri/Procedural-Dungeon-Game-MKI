using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CheeringState : AIState
{

    public AIStateID GetID()
    {
        return AIStateID.CheeringState;
    }
    public void Enter(AIAgent agent)
    {
        agent.isCheering = true;
    }
    public void Update(AIAgent agent)
    {

    }
    public void Exit(AIAgent agent)
    {
        agent.isCheering = false;
    }
}
