using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionsToActionController : MonoBehaviour
{

    AIAgent agent;
    AIEmotionSimulator emotionSimulator;

    private void Awake()
    {
        agent = GetComponent<AIAgent>();
        emotionSimulator = GetComponent<AIEmotionSimulator>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (agent.stateMachine.currentState == AIStateID.EBA_AttackState && emotionSimulator.currentEmotion == AIEmotionTypes.Terrified)
        {
            agent.stateMachine.ChangeState(AIStateID.EBA_FleeState);
        }
    }
}
