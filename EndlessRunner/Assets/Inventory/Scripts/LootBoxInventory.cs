using System;
using System.Collections.Generic;
using System.Linq;
using Item;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

namespace Inventory.Scripts
{
    public class LootBoxInventory : ILootBoxInventory
    {
        private readonly IItemFactory _itemFactory;

        public bool IsFull //Shop need this one to check if the loot box inventory is full or not
        {
            get
            {
                foreach (var lootBoxData in _slots)
                {
                    if (lootBoxData == null) return false;
                }

                return true;
            }
        }

        private ILootBoxData[] _slots;

        //This is the fixed array that stores the loot boxes
        public ILootBoxData[] Slots => _slots;
        //This is the list that stores the inventory slots

        public LootBoxInventory(IItemFactory itemFactory)
        {
            _itemFactory = itemFactory;
            _slots = new ILootBoxData[4];
        }
        
        public void AddLootBox(ILootBoxData lootBox)
        {
            for (var i = 0; i < _slots.Length; i++)
            {
                if (_slots[i] == null)//Find the first empty slot
                {
                    _slots[i] = lootBox;
                    LootBoxAdded?.Invoke(i, lootBox);
                    return;
                }
            }
        }

        public void OpenLootBox(ILootBoxData lootBox)
        {
            var slotIndex = Array.IndexOf(_slots, lootBox);
            if (slotIndex == -1) return;
            if (DateTime.UtcNow - lootBox.OpeningStartTime < lootBox.Config.TimeToOpen) return;
            _slots[slotIndex] = null;
            LootBoxRemoved?.Invoke(slotIndex, lootBox);
            //TODO: Use ItemFactory to create items
            List<IItemData> items = new List<IItemData>();
            foreach (var item in lootBox.Config.LootChances)
            {
                //Todo: Randomize the possibility to get a better item
                var itemPossibility = Random.Range(0, 100);
                items.Add(_itemFactory.CreateItem(item.itemConfig));
            }
            LootBoxOpened?.Invoke(lootBox, items.ToArray());
        }

        public void Load(IEnumerable<ILootBoxData> lootBoxes)
        {
            _slots = lootBoxes.ToArray();
        }

        public event Action<int, ILootBoxData> LootBoxAdded;
        public event Action<int, ILootBoxData> LootBoxRemoved;
        public event Action<ILootBoxData, IItemData[]> LootBoxOpened;
    }
}