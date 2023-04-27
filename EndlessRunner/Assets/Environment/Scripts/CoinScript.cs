using System.Collections;
using System.Collections.Generic;
using static RunInventoryManager;
using UnityEngine;

public class CoinScript : MonoBehaviour, IPickupable
{
    public void OnPickup()
    {
        coinAmount++;
        Destroy(gameObject);
    }
}
