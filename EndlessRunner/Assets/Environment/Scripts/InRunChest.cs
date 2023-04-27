using System.Collections;
using System.Collections.Generic;
using static RunInventoryManager;
using UnityEngine;

public class InRunChest : MonoBehaviour, IPickupable
{
    public void OnPickup()
    {
        chestAmount++;
        Destroy(gameObject);
        Debug.Log("chest amount:" + chestAmount);
    }
}