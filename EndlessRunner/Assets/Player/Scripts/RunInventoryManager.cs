using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunInventoryManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Pickupable"))
        {
            if (col.TryGetComponent(out IPickupable pickupable))
            {
                pickupable.OnPickup();
            }
            
            Debug.Log("Nice!!!");
        }
    }
}
