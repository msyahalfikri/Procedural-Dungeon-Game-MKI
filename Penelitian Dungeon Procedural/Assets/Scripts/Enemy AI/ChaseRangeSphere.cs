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


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left");
            agent.stateMachine.ChangeState(AIStateID.PatrolState);
        }

    }
}
