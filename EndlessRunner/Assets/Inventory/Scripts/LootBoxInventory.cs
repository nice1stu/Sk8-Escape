using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Scripts
{
    public class LootBoxInventory : MonoBehaviour
    {
        private static readonly BaseLootBox[] LootBoxSlots = new BaseLootBox[4];
        private readonly List<InventorySlot> _inventorySlots = new List<InventorySlot>();

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
            for (var i = 0; i < LootBoxSlots.Length; i++)
            {
                if (LootBoxSlots[i] == null)
                {
                    LootBoxSlots[i] = lootBox;
                    _inventorySlots[i].AddLootBoxIcon(lootBox);
                    return;
                }
            }
        }

        public static void RemoveLootBox(int index)
        {
            LootBoxSlots[index] = null;
        }
    }
}
