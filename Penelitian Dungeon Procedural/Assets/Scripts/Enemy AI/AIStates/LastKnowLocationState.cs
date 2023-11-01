// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// namespace EnemyAI.FSM
// {
//     public class LastKnowLocationState : AIState
//     {
//         private Vector3 playerLastKnownLocation;
//         private bool lastKnownLocationArrived;
//         float chaseTimer = 0.0f;
//         bool waiting = false;

//         public void ToInitialPost()
//         {
//             agent.navMeshAgent.speed = 2f;
//             agent.destination = enemy.initialPost.position;
//         }

//         public AIStateID GetID()
//         {
//             throw new System.NotImplementedException();
//         }

//         public void Enter(AIAgent agent)
//         {
//             agent.enabled = true;
//             agent.navMeshAgent.isStopped = false;
//             this.playerLastKnownLocation = enemy.playerLastKnowLocation;
//             lastKnownLocationArrived = false;
//         }

//         public void Update(AIAgent agent)
//         {
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

//         public void Exit(AIAgent agent)
//         {
//             throw new System.NotImplementedException();
//         }
//     }
// }
