using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int NumberOfDiamonds { get;private set; }
    public int NumberOfItems { get; private set; }
    public void DiamondCollected()
    {
        NumberOfDiamonds++;
    }
}
