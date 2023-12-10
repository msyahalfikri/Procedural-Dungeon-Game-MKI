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
        //Movement
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
        animator.SetBool("HasTurned", agent.hasTurned);
        if (agent.hasTurned)
        {
            animator.SetBool("TurnRight", agent.turnedRight);
            animator.SetBool("TurnLeft", agent.turnedLeft);
        }
        //Attack
        animator.SetBool("AttackLeft", agent.attackLeft);
        animator.SetBool("AttackRight", agent.attackRight);
        animator.SetBool("HeavyAttack", agent.heavyAttack);
        //States
        animator.SetBool("IsInAttackRange", agent.isInAttackRange);
        animator.SetBool("IsDying", agent.isDying);
        animator.SetBool("AlreadyAttacked", agent.alreadyAttacked);
        animator.SetBool("IsBlocking", agent.isBlocking);

        if (agent.BlockNow)
        {
            animator.SetLayerWeight(1, 1f);
            StartCoroutine(BlockPoseHold());
        }
        else
        {
            animator.SetLayerWeight(1, 0f);
        }

        if (agent.TakingDamage)
        {
            animator.SetTrigger("TakeDamage");
            agent.TakingDamage = false;
        }
        animator.SetBool("IsRoaring", agent.isRoaring);

        IEnumerator BlockPoseHold()
        {
            yield return new WaitForSeconds(0.5f);
            agent.BlockNow = false;
        }
    }
}

