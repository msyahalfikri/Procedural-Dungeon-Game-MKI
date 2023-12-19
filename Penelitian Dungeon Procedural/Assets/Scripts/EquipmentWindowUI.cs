using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonLiberation
{
    public class EquipmentWindowUI : MonoBehaviour
    {
        public bool rightHandSlot01Selected;
        public bool rightHandSlot02Selected;
        public bool rightHandSlot03Selected;
        public bool leftHandSlot01Selected;
        public bool leftHandSlot02Selected;
        public bool leftHandSlot03Selected;

        public HandEquipmentSlotUI[] handEquipmentSlotUI;

        private void Start()
        {
        }

        public void LoadWeaponOnEquipmentScreen(PlayerInventory playerInventory)
        {
            for (int i = 0; i < handEquipmentSlotUI.Length; i++)
            {
                if (handEquipmentSlotUI[i].rightHandSlot01)
                {
                    handEquipmentSlotUI[i].AddItem(playerInventory.weaponsInRightHandSlots[0]);
                }
                else if (handEquipmentSlotUI[i].rightHandSlot02)
                {
                    handEquipmentSlotUI[i].AddItem(playerInventory.weaponsInRightHandSlots[1]);
                }
                else if (handEquipmentSlotUI[i].rightHandSlot03)
                {
                    handEquipmentSlotUI[i].AddItem(playerInventory.weaponsInRightHandSlots[2]);
                }
                else if (handEquipmentSlotUI[i].leftHandSlot01)
                {
                    handEquipmentSlotUI[i].AddItem(playerInventory.weaponsInLeftHandSlots[0]);
                }
                else if (handEquipmentSlotUI[i].leftHandSlot02)
                {
                    handEquipmentSlotUI[i].AddItem(playerInventory.weaponsInLeftHandSlots[1]);
                }
                else if (handEquipmentSlotUI[i].leftHandSlot03)
                {
                    handEquipmentSlotUI[i].AddItem(playerInventory.weaponsInLeftHandSlots[2]);
                }
            }
        }

        public void SelectRightHandSlot01()
        {
            rightHandSlot01Selected = true;
        }
        public void SelectRightHandSlot02()
        {
            rightHandSlot02Selected = true;
        }
        public void SelectRightHandSlot03()
        {
            rightHandSlot03Selected = true;
        }
        public void SelectLeftHandSlot01()
        {
            leftHandSlot01Selected = true;
        }
        public void SelectLeftHandSlot02()
        {
            leftHandSlot02Selected = true;
        }
        public void SelectLeftHandSlot03()
        {
            leftHandSlot03Selected = true;
        }

    }
}
