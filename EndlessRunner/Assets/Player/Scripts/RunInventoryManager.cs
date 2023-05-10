using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Player
{
    public class RunInventoryManager : MonoBehaviour
    {
        public static int coinAmount;
        public static int chestAmount;
        public static int multiplier = 1;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Pickupable"))
            {
                if (col.TryGetComponent(out IPickupable pickupable))
                {
                    pickupable.OnPickup();
                }
            }
        }
        
        public int GetCoinAmount()
        {
            return coinAmount;
        }

        public void SetCoinAmount(int set)
        {
            coinAmount = set;
        }
        

        public int GetChestAmount()
        {
            return chestAmount;
        }

        
        
    }
}