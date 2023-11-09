using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInventory : MonoBehaviour
    
{
    public InventoryObject inventory;
    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Diamonds>();
        if (item)
        {
            inventory.AddItem(item.item, 1);
            Destroy(other.gameObject);
        }
    }
    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }
}
