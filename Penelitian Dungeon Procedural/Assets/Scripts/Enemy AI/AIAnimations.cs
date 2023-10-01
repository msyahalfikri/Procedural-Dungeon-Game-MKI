using System.Collections;
using System.Collections.Generic;
using EnemyAI.FSM;
using UnityEngine;
using UnityEngine.AI;

public class AIAnimations : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }


}

