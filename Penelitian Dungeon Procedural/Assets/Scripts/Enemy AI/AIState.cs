using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIStateID
{
    //States for FSM Only AI Enemy
    TestingState,
    ChasePlayer,
    IdleState,
    AttackState,
    BlockingState,
    DeathState,
    PatrolState,
    FleeState,
    RoarState,

    //States for Emotion-Based Agent AI Enemy
    EBA_ChasePlayer,
    EBA_IdleState,
    EBA_AttackState,
    EBA_BlockingState,
    EBA_DeathState,
    EBA_PatrolState,
    EBA_FleeState,
    EBA_RoarState
}
public interface AIState
{
    AIStateID GetID();
    void Enter(AIAgent agent);
    void Update(AIAgent agent);
    void Exit(AIAgent agent);
}
