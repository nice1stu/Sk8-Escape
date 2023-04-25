using UnityEngine;

namespace Inventory.Scripts
{
    public class LootBoxInventory : MonoBehaviour
    {
        [ContextMenu("Add Loot Box")]

        void AddLootBox()
        {
            foreach (var slot in lootBoxSlots)
            {
                if (slot == null)
                {
                    BaseItem.coins++;
                    Debug.Log($"Coins added, {BaseItem.coins}");
                    return;
                }
            }
        }
        
        public BaseLootBox[] lootBoxSlots = new BaseLootBox[4];
    }
}
