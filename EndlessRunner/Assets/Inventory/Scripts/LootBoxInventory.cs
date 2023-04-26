using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory.Scripts
{
    public class LootBoxInventory : MonoBehaviour
    {
        private static BaseLootBox[] _lootBoxSlots = new BaseLootBox[4];
        private List<InventorySlot> _inventorySlots = new List<InventorySlot>();

        private void Start()
        {
            _inventorySlots.AddRange(GetComponentsInChildren<InventorySlot>());
        }

        [ContextMenu("Add Loot Box")]
        void Add()
        {
            AddLootBox(FindObjectOfType<BaseLootBox>());
        }

        void AddLootBox(BaseLootBox lootBox)
        {
            for (var i = 0; i < _lootBoxSlots.Length; i++)
            {
                if (_lootBoxSlots[i] == null)
                {
                    BaseItem.coins++;
                    Debug.Log($"Coins added, {BaseItem.coins}");
                    _lootBoxSlots[i] = lootBox;
                    _inventorySlots[i].AddLootBoxIcon(lootBox);
                    return;
                }
            }
        }

        public static void RemoveLootBox(int index)
        {
            if (_lootBoxSlots[index] != null)
            {
                BaseItem.coins--;
                Debug.Log($"Coins added, {BaseItem.coins}");
                _lootBoxSlots[index] = null;
            }
        }
    }
}
