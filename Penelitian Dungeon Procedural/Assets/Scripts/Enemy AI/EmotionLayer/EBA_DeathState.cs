using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EBA_DeathState : AIState
{
    float animationWaitTime;
    public AIStateID GetID()
    {
        return AIStateID.EBA_DeathState;
    }
    public void Enter(AIAgent agent)
    {
        animationWaitTime = agent.config.deathAnimationWaitTime;
        agent.healthBarAndEmotion.gameObject.SetActive(false);
        agent.bodyIK.enabled = false;
        agent.isDying = true;
    }
    public void Update(AIAgent agent)
    {
        animationWaitTime -= Time.deltaTime;
        if (animationWaitTime < 0.0f)
        {
            agent.DestroyThisEnemy();
        }
    }
    public void Exit(AIAgent agent)
    {
        agent.isDying = false;
    }
}