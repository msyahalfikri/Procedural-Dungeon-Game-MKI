using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ChaseRangeSphere : MonoBehaviour
{
    [HideInInspector] public Vector3 lastKnownPlayerPosition;
    AIAgent agent;
    AIEmotionSimulator emotionSimulator;

    private void Start()
    {
        agent = GetComponentInParent<AIAgent>();
        emotionSimulator = GetComponent<AIEmotionSimulator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            agent.isInChaseRange = true;
        }
        Debug.Log("In Chase Range");
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            agent.isInChaseRange = false;
            if (agent.stateMachine.currentState == AIStateID.ChasePlayer)
            {
                agent.stateMachine.ChangeState(AIStateID.IdleState);
            }
            else if (agent.stateMachine.currentState == AIStateID.EBA_ChasePlayer && emotionSimulator.currentEmotion != AIEmotionTypes.Determined)
            {
                agent.stateMachine.ChangeState(AIStateID.EBA_IdleState);
            }
            else if (agent.stateMachine.currentState == AIStateID.EBA_ChasePlayer && emotionSimulator.currentEmotion == AIEmotionTypes.Determined)
            {
                agent.stateMachine.ChangeState(AIStateID.EBA_ChasePlayer);
            }
            Debug.Log("Out of Chase Range");
        }

    }
}
