using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FSM;

namespace EnemyAI.FSM
{
    [RequireComponent(typeof(Animator), typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour
    {
        private StateMachine<EnemyState, StateEvent> enemyFSM;
        private Animator animator;
        private NavMeshAgent agent;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            enemyFSM = new StateMachine<EnemyState, StateEvent>();

            // enemyFSM.AddState(EnemyState.Idle, new IdleState(false, this));
            // enemyFSM.AddState(EnemyState.Chase, new IdleState(false, this));
            // enemyFSM.AddState(EnemyState.AttackPunch, new IdleState(false, this));

            enemyFSM.SetStartState(EnemyState.Idle);
            enemyFSM.Init();
        }
        private void Update()
        {
            enemyFSM.OnLogic();
        }
    }
}