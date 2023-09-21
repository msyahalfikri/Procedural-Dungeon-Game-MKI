using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyAI.FSM
{
    public class ChaseState : EnemyStateBase
    {
        public float maxTime = 1.0f;
        public float maxDistance = 1.0f;
        float chaseTimer = 0.0f;
        private Transform target;
        public float turnDetectionThreshold = 30.0f; // Adjust this value as needed
        public bool isTurning = false;
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
                    if (sqrDistance > maxDistance * maxDistance)
                    {
                        agent.destination = target.position;
                    }

                    // Calculate the angle between the agent's forward direction and the direction to the player.
                    Vector3 toPlayer = target.position - agent.transform.position;
                    float angleToPlayer = Vector3.Angle(agent.transform.forward, toPlayer);

                    // Check if the angle exceeds the turn detection threshold.
                    if (angleToPlayer > turnDetectionThreshold)
                    {
                        isTurning = true;
                    }
                    else
                    {
                        isTurning = false;
                    }
                    // Set the "IsTurning" parameter in the Animator.
                    animator.SetBool("IsTurning", isTurning);
                    animator.SetFloat("Speed", agent.velocity.magnitude);
                    Debug.Log(isTurning);
                    chaseTimer = maxTime;
                }
            }
            else if (agent.remainingDistance <= agent.stoppingDistance)
            {
                fsm.StateCanExit();
            }
        }
    }

}
