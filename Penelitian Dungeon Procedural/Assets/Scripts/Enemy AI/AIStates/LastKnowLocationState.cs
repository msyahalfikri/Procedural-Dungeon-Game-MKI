// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// namespace EnemyAI.FSM
// {
//     public class LastKnowLocationState : EnemyStateBase
//     {
//         private Vector3 playerLastKnownLocation;
//         public delegate void PlayerExitEvent();
//         public event PlayerExitEvent onPlayerExit;
//         private bool lastKnownLocationArrived;
//         float chaseTimer = 0.0f;
//         bool waiting = false;

//         public LastKnowLocationState(bool needsExitTime, Enemy enemy, Vector3 lastKnownLocation) : base(needsExitTime, enemy)
//         {
//             this.playerLastKnownLocation = lastKnownLocation;
//         }
//         public override void OnEnter()
//         {
//             base.OnEnter();
//             agent.enabled = true;
//             agent.isStopped = false;
//             this.playerLastKnownLocation = enemy.playerLastKnowLocation;
//             lastKnownLocationArrived = false;
//         }
//         public override void OnLogic()
//         {
//             base.OnLogic();
//             if (!requestedExit)
//             {
//                 chaseTimer -= Time.deltaTime;
//                 if (chaseTimer < 0.0f)
//                 {
//                     float sqrDistance = (playerLastKnownLocation - agent.destination).sqrMagnitude;
//                     if (sqrDistance > enemy.agentConfig.maxDistance * enemy.agentConfig.maxDistance)
//                     {
//                         agent.destination = playerLastKnownLocation;
//                     }
//                     chaseTimer = enemy.agentConfig.maxTime;
//                 }

//                 if (agent.remainingDistance <= agent.stoppingDistance)
//                 {
//                     enemy.waitTimer -= Time.deltaTime;
//                     if (enemy.waitTimer <= 0.0f)
//                     {
//                         Debug.Log("back to post!");
//                         lastKnownLocationArrived = true;
//                         fsm.StateCanExit();
//                     }

//                 }
//             }

//             if (lastKnownLocationArrived)
//             {
//                 ToInitialPost();
//             }


//         }

//         public void ToInitialPost()
//         {
//             agent.speed = 2f;
//             agent.destination = enemy.initialPost.position;
//         }
//     }
// }
