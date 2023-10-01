using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI.FSM
{
    public class IdleState : EnemyStateBase
    {
        public IdleState(bool needsExitTime, Enemy enemy) : base(needsExitTime, enemy) { }

        public override void OnEnter()
        {
            base.OnEnter();
            // agent.isStopped = true;

            Vector3 playerDirection = enemy.player.position - agent.transform.position;
            if (playerDirection.magnitude > enemy.agentConfig.mightSightDistance)
            {
                return;
            }

            Vector3 agentDirection = agent.transform.forward;
            playerDirection.Normalize();
            float dotProduct = Vector3.Dot(playerDirection, agentDirection);
            if (dotProduct > 0.0f)
            {
                //change to chaseplayer state
            }
        }
    }

}
