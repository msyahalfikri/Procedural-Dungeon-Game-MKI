using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AnimationOffset : MonoBehaviour
{
    // Assuming you have a reference to the GameObject with the Animator component
    AIAgent agent;
    RigBuilder rigBuilder;
    void Start()
    {
        agent = GetComponentInParent<AIAgent>();
        rigBuilder = GetComponent<RigBuilder>();
    }
    // Modify the rotation during the animation
    void LateUpdate()
    {
        // Check if the enemy is in the blocking animation
        if (agent.isBlocking)
        {
            rigBuilder.enabled = true;
        }
        else
        {
            rigBuilder.enabled = false;
        }
    }

}
