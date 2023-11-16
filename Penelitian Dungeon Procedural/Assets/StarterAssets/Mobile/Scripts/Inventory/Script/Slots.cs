using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slots : MonoBehaviour
{
    public ItemObject itemInSlot;
    public int amountInSlot;

    RawImage icon;
    TextMeshProUGUI txt_amount;

    public void SetStats()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        icon = GetComponentInChildren<RawImage>();
        txt_amount = GetComponentInChildren<TextMeshProUGUI>();

        icon.texture = itemInSlot.icon;
        txt_amount.text = $"{amountInSlot}x";
    }
}
