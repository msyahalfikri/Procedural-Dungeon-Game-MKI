using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EmotionController : MonoBehaviour
{
    private FuzzyLogicModule fuzzyLogic;
    private TriggerManager triggerManager;
    private string currentEmotion;

    private void Start()
    {
        fuzzyLogic = GetComponent<FuzzyLogicModule>();
        triggerManager = GetComponent<TriggerManager>();
        currentEmotion = "Neutral"; // Initial emotion
    }

    // Method to receive triggers from TriggerManager
    public void ReceiveTrigger(TriggerManager.TriggerType triggerType)
    {
        // Process the trigger and update emotions using fuzzy logic
        string newEmotion = fuzzyLogic.CalculateEmotion(triggerType, currentEmotion);

        if (newEmotion != currentEmotion)
        {
            currentEmotion = newEmotion;
            Debug.Log("Emotion changed to: " + currentEmotion);

            // Optionally, trigger AI behavior based on the updated emotion
            // Call AI Controller method to adjust behavior
        }
    }
}


