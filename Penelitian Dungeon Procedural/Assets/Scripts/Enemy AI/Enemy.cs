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
        public Transform player;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            enemyFSM = new StateMachine<EnemyState, StateEvent>();

            enemyFSM.AddState(EnemyState.Idle, new IdleState(false, this));
            enemyFSM.AddState(EnemyState.Chase, new ChaseState(false, this, player));
            enemyFSM.AddState(EnemyState.AttackPunch, new AttackPunchState(false, this, player));

            enemyFSM.SetStartState(EnemyState.Chase);
            enemyFSM.Init();
        }
        private void Update()
        {
            enemyFSM.OnLogic();
        }
    }
}