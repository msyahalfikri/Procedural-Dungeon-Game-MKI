using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Weapon Object", menuName = "Inventory System/Items/Equipment")]
public class EquipmentObject : ItemObject

{
    public float atkBonus;
    public float defBonus;
    public void Awake()
    {
        type = ItemType.Equipment;
    }
}
