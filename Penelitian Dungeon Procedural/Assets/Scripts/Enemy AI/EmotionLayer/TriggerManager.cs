using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    // Define trigger types (could be enum or constants)
    public enum TriggerType
    {
        PlayerAttack,
        SightDetected,
        // Other triggers relevant to your game
    }

    // Method to detect triggers and inform EmotionController
    public void DetectTrigger(TriggerType triggerType)
    {
        // Detect trigger occurrence in the game environment
        // Call EmotionController's ReceiveTrigger method with the detected trigger
        GetComponent<EmotionController>().ReceiveTrigger(triggerType);
    }

    // Other trigger detection methods
}
