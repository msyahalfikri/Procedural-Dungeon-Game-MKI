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
        private NavMeshAgent agent;
        public Transform player;
        public AiAgentConfig agentConfig;
        public Transform initialPost;
        public float waitTimer = 5f;

        private bool isInAttackRange;
        private bool isInChaseRange;

        private AISensor aiSensor;
        private ChaseSensor chaseSensor;

        private Vector3 playerLastKnowLocation;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            enemyFSM = new StateMachine<EnemyState, StateEvent>();
            aiSensor = GetComponent<AISensor>();
            chaseSensor = GetComponentInChildren<ChaseSensor>();

            //Add States
            enemyFSM.AddState(EnemyState.Idle, new IdleState(false, this));
            enemyFSM.AddState(EnemyState.Chase, new ChaseState(false, this, player));
            enemyFSM.AddState(EnemyState.AttackPunch, new AttackPunchState(false, this, OnAttack));
            enemyFSM.AddState(EnemyState.GoToLastKnownLocation, new LastKnowLocationState(false, this, playerLastKnowLocation));

            //Add Transitions
            enemyFSM.AddTriggerTransition(StateEvent.DetectPlayer, new Transition<EnemyState>(EnemyState.Idle, EnemyState.Chase));
            enemyFSM.AddTriggerTransition(StateEvent.LostPlayer, new Transition<EnemyState>(EnemyState.Chase, EnemyState.GoToLastKnownLocation));

            //Initial State
            enemyFSM.SetStartState(EnemyState.Idle);
            enemyFSM.Init();
        }
        private void Start()
        {
            aiSensor.onPlayerEnter += FollowPlayerSensor_OnPlayerEnter;
            chaseSensor.onPlayerExit += FollowPlayerSensor_OnPlayerExit;

        }

        private void FollowPlayerSensor_OnPlayerExit(Vector3 lastKnownPosition)
        {
            playerLastKnowLocation = lastKnownPosition;
            enemyFSM.Trigger(StateEvent.LostPlayer);
            isInChaseRange = false;
        }
        private void FollowPlayerSensor_OnPlayerEnter(Transform player)
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