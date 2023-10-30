// using UnityEngine;
// using System.Collections.Generic;
// using EnemyAI.FSM;

// public class PatrolState : EnemyStateBase
// {
//     bool walkPointSet;
//     private Vector3 walkPoint;

//     public PatrolState(bool needsExitTime, Enemy enemy) : base(needsExitTime, enemy) { }

//     public override void OnEnter()
//     {
//         base.OnEnter();
//         agent.enabled = true;
//         agent.isStopped = false;

//     }

//     public override void OnLogic()
//     {
//         base.OnLogic();
//         Patrolling();
//     }


//     private void SearchWalkPoint()
//     {
//         float randomZ = Random.Range(-enemy.walkPointRange, enemy.walkPointRange);
//         float randomX = Random.Range(-enemy.walkPointRange, enemy.walkPointRange);
//         walkPoint = new Vector3(agent.transform.position.x + randomX, agent.transform.position.y, agent.transform.position.z + randomZ);

//         if (Physics.Raycast(walkPoint, -agent.transform.up, 2f, enemy.Ground))
//         {
//             walkPointSet = true;
//         }
//     }

//     private void Patrolling()
//     {
//         Debug.Log(walkPointSet);
//         if (!walkPointSet) SearchWalkPoint();

//         if (walkPointSet) agent.SetDestination(walkPoint);

//         Vector3 distanceToWalkPoint = agent.transform.position - walkPoint;

//         if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;
//     }
// }
