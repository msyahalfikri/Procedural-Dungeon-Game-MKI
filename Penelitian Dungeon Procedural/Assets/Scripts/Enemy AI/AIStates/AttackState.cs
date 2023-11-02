using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackPunchState : AIState
{
    public AIStateID GetID()
    {
        return AIStateID.AttackState;
    }
    public void Enter(AIAgent agent)
    {

    }

    public void Exit(AIAgent agent)
    {
        throw new NotImplementedException();
    }


    public void Update(AIAgent agent)
    {
        throw new NotImplementedException();
    }
}

