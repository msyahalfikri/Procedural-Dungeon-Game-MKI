using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyAI.FSM
{
    public class ChaseState : EnemyStateBase
    {

        float chaseTimer = 0.0f;
        private Transform target;
        public ChaseState(bool needsExitTime, Enemy enemy, Transform target) : base(needsExitTime, enemy)
        {
            this.target = target;
        }
        public override void OnEnter()
        {
            base.OnEnter();
            agent.enabled = true;
            agent.isStopped = false;
            // animator.Play(stateName: Walk);
        }

        public override void OnLogic()
        {
            base.OnLogic();
            if (!requestedExit)
            {
                chaseTimer -= Time.deltaTime;
                if (chaseTimer < 0.0f)
                {
                    float sqrDistance = (target.position - agent.destination).sqrMagnitude;
                    if (sqrDistance > enemy.agentConfig.maxDistance * enemy.agentConfig.maxDistance)
                    {
                        agent.destination = target.position;
                    }
                    chaseTimer = enemy.agentConfig.maxTime;
                }
            }
            else if (agent.remainingDistance <= agent.stoppingDistance)
            {
                fsm.StateCanExit();
            }
        }
    }

}
