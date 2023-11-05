using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DeathState : AIState
{
    float animationWaitTime;
    public AIStateID GetID()
    {
        return AIStateID.DeathState;
    }
    public void Enter(AIAgent agent)
    {
        animationWaitTime = agent.config.deathAnimationWaitTime;
        agent.healthBar.gameObject.SetActive(false);
        agent.bodyIK.enabled = false;
        agent.isDying = true;
    }
    public void Update(AIAgent agent)
    {
        animationWaitTime -= Time.deltaTime;
        if (animationWaitTime < 0.0f)
        {
            agent.ragdoll.ActivateRagdoll();
            agent.mesh.updateWhenOffscreen = true;
        }
    }
    public void Exit(AIAgent agent)
    {
        agent.isDying = false;
    }
}