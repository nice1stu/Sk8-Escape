using System;
using System.Collections.Generic;
using System.Linq;
using Item;
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
            List<IItemData> items = new List<IItemData>();
            var item = ProbabilityCheck(lootBox);
            items.Add(item);
            LootBoxOpened?.Invoke(lootBox, items.ToArray());
        }

        public void Load(IEnumerable<ILootBoxData> lootBoxes)
        {
            _slots = lootBoxes.ToArray();
        }

        private IItemData ProbabilityCheck (ILootBoxData lootBox)
        {
            var weights = lootBox.Config.LootChances;
            int totalWeight = 0;
            foreach (var configLootChance in weights)
            {
                totalWeight += configLootChance.chance;
            }
            int randomWeight = Random.Range(0, totalWeight);
            for (int i = 0;i < weights.Length; ++i)
            {
                randomWeight -= weights[i].chance;
                if (randomWeight < 0)
                {
                    IItemData item = _itemFactory.CreateItem(weights[i].itemConfig);
                    return item;
                }
            }

            return null;
        }

        

        public event Action<int, ILootBoxData> LootBoxAdded;
        public event Action<int, ILootBoxData> LootBoxRemoved;
        public event Action<ILootBoxData, IItemData[]> LootBoxOpened;
    }
}