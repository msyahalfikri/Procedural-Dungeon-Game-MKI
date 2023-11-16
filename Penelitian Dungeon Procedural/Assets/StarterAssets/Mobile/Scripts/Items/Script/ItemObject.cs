using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Create New Item")]
[System.Serializable]
public class ItemObject : ScriptableObject
{
    public int Id;

    public string itemName;

    public enum ItemType
{
    Food,
    Equipment,
    Default
}
    public enum Rarity
    {
        common,
        uncommon,
        rare,
        epic,
        legendary
    };

    public GameObject prefab;
    public Texture icon;
    public ItemType type;
    [TextArea(15, 20)]
    public string description;
    public Rarity rarity;
    public int maxStack;
    public float weight;
    public int baseValue;
}
