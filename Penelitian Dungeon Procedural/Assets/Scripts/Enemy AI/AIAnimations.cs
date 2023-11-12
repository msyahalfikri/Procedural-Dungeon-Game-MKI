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
        animator.SetBool("IsInAttackRange", agent.isInAttackRange);
        animator.SetBool("AlreadyAttacked", agent.alreadyAttacked);


        animator.SetBool("AttackLeft", agent.attackLeft);
        animator.SetBool("AttackRight", agent.attackRight);
        animator.SetBool("HeavyAttack", agent.heavyAttack);

        //Turned Animation
        animator.SetBool("HasTurned", agent.hasTurned);
        if (agent.hasTurned)
        {
            animator.SetBool("TurnRight", agent.turnedRight);
            animator.SetBool("TurnLeft", agent.turnedLeft);
        }
        animator.SetBool("IsBlocking", agent.isBlocking);

        if (agent.TakingDamage)
        {
            animator.SetTrigger("TakeDamage");
            agent.TakingDamage = false;
        }

    }


}

