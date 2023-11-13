using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AiAgentConfig : ScriptableObject
{
    public float agentWalkSpeed = 2.0f;
    public float agentRunSpeed = 4.5f;
    public float maxTime = 1.0f;
    public float maxDistance = 1.0f;
    public float maxSightDistance = 5.0f;
    public float walkPointRange = 10.0f;
    public float IdleWaitTime = 3.0f;
    public float deathAnimationWaitTime = 3.0f;
    public float timeBetweenAttacks = 4.0f;
    public float heavyAttackCooldownMaxTime = 10.0f;
    public float observingRange = 8.0f;
    public float attackRange = 8.0f;
    public float blockTimer = 10.0f;
    public float blockChanceInPercent = 30.0f;
    public float roarAnimationMaxTime = 6.0f;

}
