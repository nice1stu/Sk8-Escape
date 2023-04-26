using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour, IPickupable
{
    public void OnPickup()
    {
        Destroy(gameObject);
    }
}
