using System;
using UnityEngine;

namespace Inventory.Scripts
{
    public class LootBoxInfoSO : MonoBehaviour
    {
        public LootBoxConfigSo lootBoxConfigSo;
        
        public void PurchasedChest()
        {
            // Integration add logic when buying
            Debug.Log("Chest Unlock!");
            var lootBoxData = new LootBoxData(lootBoxConfigSo, DateTime.UtcNow);
            Dependencies.Instance.LootBoxes.AddLootBox(lootBoxData);
        }
    }
}
