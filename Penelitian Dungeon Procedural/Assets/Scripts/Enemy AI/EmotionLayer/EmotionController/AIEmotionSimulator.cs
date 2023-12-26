using Unity.Collections;
using UnityEngine;

public class AIEmotionSimulator : MonoBehaviour
{
    //References & Triggers
    AIAgent agent;
    AIHealth aiHealth;
    PlayerHealth playerHealth;

    [ReadOnly]
    public AIEmotionTypes currentEmotion;

    //weight/sensitivity for emotions
    public float angerSensitivity = 1.0f;
    public float fearSensitivity = 1.0f;
    public float determinationSensitivity = 1.0f;

    //Fuzzy Values: Player Distance to Enemy
    float playerCloseMembership;
    float playerMediumMembership;
    float playerFarMembership;

    //Fuzzy Values: Enemy's Heath Point
    float enemyLowHealthMembership;
    float enemyMediumHealthMembership;
    float enemyHighHealthMembership;

    //Fuzzy Values: Player's Heath Point
    float playerLowHealthMembership;
    float playerMediumHealthMembership;
    float playerHighHealthMembership;

    //Fuzzy Values: Player's Heath Point
    float enemyLowAngerMembership;
    float enemyMediumAngerMembership;
    float enemyHighAngerMembership;


    //Inference Rules for: Fear Emotion
    float ruleLowFear1;
    float ruleLowFear2;
    float ruleMedFear1;
    float ruleMedFear2;
    float ruleHighFear;

    //Inference Rules for: Anger Emotion
    float ruleLowAnger;
    float ruleMedAnger;
    float ruleHighAnger;

    //Inference Rules for: Determination Emotion
    float ruleLowDetermination;
    float ruleMedDetermination;
    float ruleHighDetermination;

    float weightedFearVal;
    float weightedAngerVal;
    float weightedDeterminationVal;



    private void Awake()
    {
        agent = GetComponent<AIAgent>();
        aiHealth = GetComponent<AIHealth>();
    }
    private void Start()
    {
        playerHealth = agent.playerTransform.GetComponentInChildren<PlayerHealth>();
    }
    private void Update()
    {
        CalculateEnemyHealthMembership(aiHealth.currentHealth, aiHealth.maxHealth);
        CalculatePlayerHealthMembership(playerHealth.currentHealth, playerHealth.maxHealth);
        CalculateDistanceMembership(Vector3.Distance(agent.transform.position, agent.playerTransform.transform.position));


        InferenceEngineFearEmotion();
        InferenceEngineAngerEmotion();
        InferenceEngineDetermineEmotion();

        InterpretAndUpdateEmotion();

        // Debug.Log("Fear: " + DefuzzifyToFearCrispValue() +
        // "|| Distance: " + Vector3.Distance(agent.transform.position, agent.playerTransform.transform.position) + "|| Health: " + aiHealth.currentHealth + "|| CurrentEmotion: " + currentEmotion);
        // Debug.Log("Fuzzy health: [" + ruleHighAnger + ", " + ruleMedAnger + ", " + ruleLowAnger + "] || Health: " + aiHealth.currentHealth);
        // Debug.Log(DefuzzifyToAngerCrispValue() + "|| Health: " + aiHealth.currentHealth + " ||Max: " + aiHealth.maxHealth);
        // Debug.Log(DefuzzifyToDeterminationCrispValue() + "|| Health: " + playerHealth.currentHealth);
        // Debug.Log("Anger: " + weightedAngerVal + "|| Fear: " + weightedFearVal + "|| Determination: " + weightedDeterminationVal + "|| CurrentEmotion: " + currentEmotion);
    }

    //Gaussian Membership function
    float GaussianMembership(float x, float mean, float standardDeviation)
    {
        float exponent = -((x - mean) * (x - mean)) / (2 * standardDeviation * standardDeviation);
        return Mathf.Exp(exponent);
    }

    //Triangular Membership function
    float TriangularMembership(float x, float lowerLimit, float center, float upperLimit)
    {
        if (x <= lowerLimit || x >= upperLimit)
        {
            return 0f;
        }
        else if (x >= center)
        {
            return 1 - (x - center) / (upperLimit - center);
        }
        else
        {
            return 1 - (center - x) / (center - lowerLimit);
        }
    }

    float CalculateFearFromRules(float inputA, float inputB, string emotion)
    {
        if (emotion == "Low")
            return Mathf.Min(inputA, inputB);
        else if (emotion == "Medium")
            return Mathf.Min(inputA, inputB);
        else if (emotion == "High")
            return Mathf.Min(inputA, inputB);

        return 0f;
    }


    // Fuzzification for player distance
    void CalculateDistanceMembership(float distance)
    {
        float closeMean = 5f;
        float mediumMean = 15f;
        float farMean = 25f;
        float standardDeviation = 3f; // Adjust this for the width of the curve

        playerCloseMembership = GaussianMembership(distance, closeMean, standardDeviation);
        playerMediumMembership = GaussianMembership(distance, mediumMean, standardDeviation);
        playerFarMembership = GaussianMembership(distance, farMean, standardDeviation);
        // Debug.Log("Distance: " + distance + "|| Close: " + playerCloseMembership + " || Medium: " + playerMediumMembership + "|| far: " + playerFarMembership);
    }

    // Fuzzification for Enemy's Health
    void CalculateEnemyHealthMembership(float health, float maxHealth)
    {
        float adjustment = 0.0001f;
        float lowLimit = 0f;
        float mediumLimit = maxHealth / 2 - adjustment;
        float highLimit = maxHealth + adjustment;

        enemyLowHealthMembership = TriangularMembership(health, lowLimit, lowLimit, mediumLimit);
        enemyMediumHealthMembership = TriangularMembership(health, lowLimit, mediumLimit, highLimit);
        enemyHighHealthMembership = TriangularMembership(health, mediumLimit, highLimit, highLimit); // Adjust upper limit
        // Debug.Log("HP: " + health + "|| LowMem: " + enemyLowHealthMembership + " || MedMem: " + enemyMediumHealthMembership + "|| HighMem: " + enemyHighHealthMembership);
    }

    // Fuzzification for Player's Health
    void CalculatePlayerHealthMembership(float health, float maxHealth)
    {
        float adjustment = 0.0001f;
        float lowLimit = 0f;
        float mediumLimit = maxHealth / 2f - adjustment;
        float highLimit = maxHealth + adjustment;

        playerLowHealthMembership = TriangularMembership(health, lowLimit, lowLimit, mediumLimit);
        playerMediumHealthMembership = TriangularMembership(health, lowLimit, mediumLimit, highLimit);
        playerHighHealthMembership = TriangularMembership(health, mediumLimit, highLimit, highLimit); // Adjust upper limit
        // Debug.Log("HP: " + health + "|| LowMem: " + playerLowHealthMembership + " || MedMem: " + playerMediumHealthMembership + "|| HighMem: " + playerHighHealthMembership);
    }

    //Inference Engine: Fear Emotion
    public void InferenceEngineFearEmotion()
    {
        // Applying fuzzy logic rules
        float fearValue = 0f;

        // Rule 1: If Player is "Close" AND enemy has "High" health THEN Fear is "Low"
        ruleLowFear1 = Mathf.Max(fearValue, CalculateFearFromRules(playerCloseMembership, enemyHighHealthMembership, "Low"));

        // Rule 2: If Player is "Far" AND enemy has "High" health THEN Fear is "Low"
        ruleLowFear2 = Mathf.Max(fearValue, CalculateFearFromRules(playerFarMembership, enemyHighHealthMembership, "Low"));

        // Rule 3: If Player is "Close" AND enemy has "Low" health THEN Fear is "High"
        ruleHighFear = Mathf.Max(fearValue, CalculateFearFromRules(playerCloseMembership, enemyLowHealthMembership, "High"));

        // Rule 4: If Player is "Far" AND enemy has "Low" health THEN Fear is "Medium"
        ruleMedFear1 = Mathf.Max(fearValue, CalculateFearFromRules(playerFarMembership, enemyLowHealthMembership, "Medium"));

        // Rule 5: If Player is "Close" AND enemy has "Medium" health THEN Fear is "Medium"
        ruleMedFear2 = Mathf.Max(fearValue, CalculateFearFromRules(playerCloseMembership, enemyMediumHealthMembership, "Medium"));
    }

    //Inference Engine: Anger Emotion
    public void InferenceEngineAngerEmotion()
    {

        // Rule 1: If Enemy has "Low" health THEN Anger is "High"
        ruleHighAnger = enemyLowHealthMembership;

        // Rule 2: If Enemy has "Medium" health THEN Anger is "Medium"
        ruleMedAnger = enemyMediumHealthMembership;

        // Rule 3: If Enemy has "High" health THEN Anger is "Low"
        ruleLowAnger = enemyHighHealthMembership;
    }

    //Inference Engine: Determination Emotion
    public void InferenceEngineDetermineEmotion()
    {
        // Rule 1: If Player has "Low" health THEN Determination is "High"
        ruleHighDetermination = playerLowHealthMembership;

        // Rule 2: If Player has "Medium" health THEN Determination is "Medium"
        ruleMedDetermination = playerMediumHealthMembership;

        // Rule 3: If Player has "High" health THEN Determination is "Low"
        ruleLowDetermination = playerHighHealthMembership;
    }

    //--------Defuzzification using Centroid Method---------

    //Defuzzification: Fear Emotion
    float DefuzzifyToFearCrispValue()
    {
        float LowFearCentroidValue = 10f;
        float MediumFearCentroidValue = 50f;
        float HighFearCentroidValue = 100f;

        float numerator = ruleLowFear1 * LowFearCentroidValue + ruleLowFear2 * LowFearCentroidValue + ruleMedFear1 * MediumFearCentroidValue + ruleMedFear2 * MediumFearCentroidValue + ruleHighFear * HighFearCentroidValue;
        float denominator = ruleLowFear1 + ruleLowFear2 + ruleMedFear1 + ruleMedFear2 + ruleHighFear;

        if (denominator != 0)
            return numerator / denominator;

        return 0f;
    }
    //Defuzzification: Anger Emotion
    float DefuzzifyToAngerCrispValue()
    {
        float lowAngerCentroidValue = 10f;
        float mediumAngerCentroidValue = 50f;
        float highAngerCentroidValue = 100f;

        float numerator = ruleLowAnger * lowAngerCentroidValue + ruleMedAnger * mediumAngerCentroidValue + ruleHighAnger * highAngerCentroidValue;
        float denominator = ruleLowAnger + ruleMedAnger + ruleHighAnger;

        if (denominator != 0)
            return numerator / denominator;

        return 0f;
    }
    //Defuzzification: Determination Emotion
    float DefuzzifyToDeterminationCrispValue()
    {
        float LowDeterminationCentroidValue = 10f;
        float MediumDeterminationCentroidValue = 50f;
        float HighDeterminationCentroidValue = 100f;

        float numerator = ruleLowDetermination * LowDeterminationCentroidValue + ruleMedDetermination * MediumDeterminationCentroidValue + ruleHighDetermination * HighDeterminationCentroidValue;
        float denominator = ruleLowDetermination + ruleMedDetermination + ruleHighDetermination;

        if (denominator != 0)
            return numerator / denominator;

        return 0f;
    }

    //interpret emotion crisp value and change current emotion
    float GetWeightedFearValue()
    {
        float crispFearVal = DefuzzifyToFearCrispValue();
        float weightedFearVal = crispFearVal * fearSensitivity;
        return weightedFearVal;
    }

    float GetWeightedAngerValue()
    {
        float crispAngerVal = DefuzzifyToAngerCrispValue();
        float weightedAngerVal = crispAngerVal * angerSensitivity;
        return weightedAngerVal;
    }

    float GetWeightedDeterminationValue()
    {
        float crispDeterminationVal = DefuzzifyToDeterminationCrispValue();
        float weightedDeterminationVal = crispDeterminationVal * determinationSensitivity;
        return weightedDeterminationVal;
    }

    void InterpretAndUpdateEmotion()
    {
        weightedFearVal = GetWeightedFearValue();
        weightedAngerVal = GetWeightedAngerValue();
        weightedDeterminationVal = GetWeightedDeterminationValue();

        //Decides emotion based on the highest value 
        if ((weightedFearVal > weightedAngerVal) && (weightedFearVal > weightedDeterminationVal))
        {
            if (weightedFearVal <= 49)
            {
                currentEmotion = AIEmotionTypes.Calm;
            }
            else if (weightedFearVal >= 50 && weightedFearVal <= 74)
            {
                currentEmotion = AIEmotionTypes.Apprehensive;
            }
            else if (weightedFearVal >= 75)
            {
                currentEmotion = AIEmotionTypes.Terrified;
            }
        }
        else if (weightedAngerVal > weightedDeterminationVal && (weightedAngerVal > weightedFearVal))
        {
            if (weightedAngerVal <= 49)
            {
                currentEmotion = AIEmotionTypes.Calm;
            }
            else if (weightedAngerVal >= 50 && weightedAngerVal <= 74)
            {
                currentEmotion = AIEmotionTypes.Annoyed;
            }
            else if (weightedAngerVal >= 75)
            {
                currentEmotion = AIEmotionTypes.Furious;
            }
        }
        else if ((weightedDeterminationVal > weightedAngerVal) && (weightedDeterminationVal > weightedFearVal))
        {
            if (weightedDeterminationVal <= 49)
            {
                currentEmotion = AIEmotionTypes.Calm;
            }
            else if (weightedDeterminationVal >= 50 && weightedDeterminationVal <= 74)
            {
                currentEmotion = AIEmotionTypes.Firm;
            }
            else if (weightedDeterminationVal >= 75)
            {
                currentEmotion = AIEmotionTypes.Determined;
            }
        }
    }
}
