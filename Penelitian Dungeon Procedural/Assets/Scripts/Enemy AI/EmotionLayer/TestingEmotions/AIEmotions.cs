using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AIEmotionTypes
{
    //---Normal Emotion---
    Calm,

    //---Fear/Scared Emotions---
    Apprehensive, //Slightly scared
    Terrified, //Very scared

    //---Angry Emotions---
    Annoyed, //Slightly angry
    Furious, //Very angry

    //---Determine Emotions---
    Firm, //Slightly Determine
    Determined //Very Determine
}

public enum TriggerTypes
{
    PlayerAttack,
    PlayerDistance,
    LowHealth,
    SurpriseAttack
}


