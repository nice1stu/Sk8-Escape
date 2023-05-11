using System;
using Item;

namespace Inventory.Scripts
{
    public interface ILootBoxInventory
    {
        ILootBoxData[] Slots { get; }
        
        void AddLootBox(ILootBoxData lootBox); // discard it, if no free slot
        void OpenLootBox(ILootBoxData lootBox); // validate that it can be opened
        
        event Action<int, ILootBoxData> LootBoxAdded;
        event Action<int, ILootBoxData> LootBoxRemoved;
        event Action<ILootBoxData, IItemData[]> LootBoxOpened;
    }
}
