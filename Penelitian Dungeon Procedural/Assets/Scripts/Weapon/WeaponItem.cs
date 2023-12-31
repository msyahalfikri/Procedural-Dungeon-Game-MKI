using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonLiberation
{
    [CreateAssetMenu(menuName = "Items/Weapon Items")]
    public class WeaponItem : Item
    {
        public GameObject modelPrefab;
        public bool isUnarmed;

        [Header("Idle Animations")]
        public string right_hand_idle;
        public string left_hand_idle;
        public string two_hand_idle;

        [Header("One Handed Attack Animations")]
        public string OH_Light_Attack_1;
        public string OH_Light_Attack_2;
        public string OH_Heavy_Attack_1;
        public string OH_Heavy_Attack_2;

        [Header("Two Handed Attack Animations")]
        public string TH_Light_Attack_1;
        public string TH_Light_Attack_2;
        public string TH_Heavy_Attack_1;

        [Header("Stamina costs")]
        public int baseStamina;
        public float lightAttackMultiplier;
        public float heavyAttackMultiplier;
    }
}
