using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAnimations : MonoBehaviour
{
    private Animator animator;
    private AIAgent agent;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<AIAgent>();
    }
    private void Update()
    {
        if (agent.isEmotionBasedAgent)
        {
            if (agent.emotionSimulator.currentEmotion == AIEmotionTypes.Furious || agent.emotionSimulator.currentEmotion == AIEmotionTypes.Determined)
            {
                animator.SetFloat("AnimSpeed", 1.2f);
            }
            else
            {
                animator.SetFloat("AnimSpeed", 1f);
            }
        }

        //Movement
        animator.SetFloat("Speed", agent.navMeshAgent.velocity.magnitude);
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

        if (agent.blockNow)
        {
            animator.SetLayerWeight(1, 1f);
            StartCoroutine(BlockPoseHold());
        }
        else
        {
            animator.SetLayerWeight(1, 0f);
        }

        if (agent.isTakingDamage)
        {
            animator.SetTrigger("TakeDamage");
        }
        animator.SetBool("IsRoaring", agent.isRoaring);

        IEnumerator BlockPoseHold()
        {
            yield return new WaitForSeconds(0.5f);
            agent.blockNow = false;
        }
    }
}

