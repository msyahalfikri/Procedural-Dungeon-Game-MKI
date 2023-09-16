using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AILocomotion : MonoBehaviour
{
    public Transform playerTransform;
    NavMeshAgent agent;
    Animator animator;
    public float maxTime = 1.0f;
    public float maxDistance = 1.0f;
    float timer = 0.0f;

    // Turn-related variables
    public float turnDetectionThreshold = 30.0f; // Adjust this value as needed
    public bool isTurning = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            float sqrDistance = (playerTransform.position - agent.destination).sqrMagnitude;
            if (sqrDistance > maxDistance * maxDistance)
            {
                agent.destination = playerTransform.position;
            }

            // Calculate the angle between the agent's forward direction and the direction to the player.
            Vector3 toPlayer = playerTransform.position - transform.position;
            float angleToPlayer = Vector3.Angle(transform.forward, toPlayer);

            // Check if the angle exceeds the turn detection threshold.
            if (angleToPlayer > turnDetectionThreshold)
            {
                isTurning = true;
            }
            else
            {
                isTurning = false;
            }

            timer = maxTime;
        }

        // Set the "Speed" parameter based on the agent's velocity.
        animator.SetFloat("Speed", agent.velocity.magnitude);

        // Set the "IsTurning" parameter in the Animator.
        animator.SetBool("IsTurning", isTurning);
        Debug.Log(isTurning);
    }
}
