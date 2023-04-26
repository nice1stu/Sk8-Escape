using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Scripts
{
    public class LootBoxInventory : MonoBehaviour
    {
        //This is the fixed array that stores the loot boxes
        private static readonly BaseLootBox[] LootBoxSlots = new BaseLootBox[4];
        //This is the list that stores the inventory slots
        private readonly List<InventorySlot> _inventorySlots = new();

        private void Start()
        {
            //On start, adds inventory slots to the list
            _inventorySlots.AddRange(GetComponentsInChildren<InventorySlot>());
        }

        //May be removed in a later stage, good for testing
        [ContextMenu("Add Loot Box")]
        
        void Add()//This function is for the context menu
        {
            AddLootBox(FindObjectOfType<BaseLootBox>());
        }

        void AddLootBox(BaseLootBox lootBox) //Adds loot boxes to the loot box inventory
        {
            for (var i = 0; i < LootBoxSlots.Length; i++)
            {
                if (LootBoxSlots[i] == null)//Find the first empty slot
                {
                    LootBoxSlots[i] = lootBox;
                    _inventorySlots[i].AddLootBoxIcon(lootBox);//Adds the loot box image to the inventory slot
                    return;
                }
            }
        }

        public static void RemoveLootBox(int index)//Removes the loot box at the index(check InventorySlot script)
        {
            LootBoxSlots[index] = null;
        }
    }
}
