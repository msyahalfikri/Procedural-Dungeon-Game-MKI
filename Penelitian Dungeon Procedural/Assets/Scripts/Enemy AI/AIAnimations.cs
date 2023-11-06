using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAnimations : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private AIAgent agent;
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent = GetComponent<AIAgent>();
    }
    private void Update()
    {
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
        animator.SetBool("IsDying", agent.isDying);
        animator.SetBool("IsInAttackRange", agent.IsInAttackRange);
        animator.SetBool("AlreadyAttacked", agent.alreadyAttacked);
        if (agent.attackLeft)
        {
            animator.SetTrigger("AttackLeft");
        }
        else if (agent.attackRight)
        {
            animator.SetTrigger("AttackRight");
        }
        if (agent.heavyAttack) animator.SetTrigger("HeavyAttack");
    }


}

