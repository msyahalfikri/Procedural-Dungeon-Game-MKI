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
        public AiAgentConfig agentConfig;

        private bool isInAttackrange;
        private bool isInChaseRange;

        private AISensor aiSensor;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            enemyFSM = new StateMachine<EnemyState, StateEvent>();
            aiSensor = GetComponent<AISensor>();

            //Add States
            enemyFSM.AddState(EnemyState.Idle, new IdleState(false, this));
            enemyFSM.AddState(EnemyState.Chase, new ChaseState(false, this, player));
            enemyFSM.AddState(EnemyState.AttackPunch, new AttackPunchState(false, this, OnAttack));

            //Add Transitions
            enemyFSM.AddTriggerTransition(StateEvent.DetectPlayer, new Transition<EnemyState>(EnemyState.Idle, EnemyState.Chase));

            enemyFSM.SetStartState(EnemyState.Idle);
            enemyFSM.Init();
        }
        private void Start()
        {
            aiSensor.onPlayerEnter += FolllowPlayerSensor_OnPlayerEnter;
            aiSensor.onPlayerExit += FolllowPlayerSensor_OnPlayerExit; ;
        }

        private void FolllowPlayerSensor_OnPlayerExit(Vector3 lastKnownPosition)
        {
            enemyFSM.Trigger(StateEvent.LostPlayer);
            isInChaseRange = false;
        }
        private void FolllowPlayerSensor_OnPlayerEnter(Transform player)
        {
            enemyFSM.Trigger(StateEvent.DetectPlayer);
            isInChaseRange = true;
        }

        private void OnAttack(State<EnemyState, StateEvent> State)
        {

        }
        private void Update()
        {
            enemyFSM.OnLogic();
        }
    }
}