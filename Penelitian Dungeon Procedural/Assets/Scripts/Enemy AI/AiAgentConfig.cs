using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AiAgentConfig : ScriptableObject
{
    public float agentWalkSpeed = 2.0f;
    public float agentRunSpeed = 3.5f;
    public float ChaseTimerMaxTime = 1.0f;
    public float maxDistance = 1.0f;
    public float walkPointRange = 10.0f;
    public float minIdleWaitTime = 3.0f;
    public float maxIdleWaitTime = 3.0f;
    public float deathAnimationWaitTime = 3.0f;
    public float timeBetweenAttacks = 4.0f;
    public float heavyAttackCooldownMaxTime = 10.0f;
    public float normalAttackDamage = 10.0f;
    public float heavyAttackDamage = 25.0f;
    public float blockTimer = 10.0f;
    public float blockChanceInPercent = 15.0f;
    public float roarAnimationMaxTime = 6.0f;
    public float retreatSpeed = 6f;
    public float retreatDistance = 20f;
}
