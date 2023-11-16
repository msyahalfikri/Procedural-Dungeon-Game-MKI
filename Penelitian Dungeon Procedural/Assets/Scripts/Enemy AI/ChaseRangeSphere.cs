using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ChaseRangeSphere : MonoBehaviour
{
    [HideInInspector] public Vector3 lastKnownPlayerPosition;
    AIAgent agent;

    private void Start()
    {
        agent = GetComponentInParent<AIAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            agent.isInChaseRange = true;
            Debug.Log("In Chase Range");
        }


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            agent.isInChaseRange = false;
            if (agent.stateMachine.currentState == AIStateID.ChasePlayer)
            {
                agent.stateMachine.ChangeState(AIStateID.IdleState);
                Debug.Log("Out of Chase Range");
            }

        }

    }
}
