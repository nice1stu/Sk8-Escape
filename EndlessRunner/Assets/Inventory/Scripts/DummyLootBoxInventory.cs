using System;
using Item;
using UnityEngine;
using UnityEngine.Serialization;

namespace Inventory.Scripts
{
    [Serializable]
    public class DummyLootBoxInventory : ILootBoxInventory
    {
        [Serializable]
        public class DummyLootBoxData : ILootBoxData
        {
            [SerializeField] private LootBoxConfigSo config;
            [SerializeField] private DateTime openingStartTime;
            public LootBoxConfigSo Config => config;
            ILootBoxConfig ILootBoxData.Config => config;
            public DateTime OpeningStartTime => openingStartTime;
        }

        [SerializeField] private DummyLootBoxData openEventLootBox;
        [SerializeField] private ItemData[] openEventItems;
        
        public ILootBoxData[] Slots { get; }
        public void AddLootBox(ILootBoxData lootBox)
        {
            throw new NotImplementedException();
        }

        public void OpenLootBox(ILootBoxData lootBox)
        {
            throw new NotImplementedException();
        }

        public event Action<int, IItemData> LootBoxAdded;
        public event Action<int, IItemData> LootBoxRemoved;
        public event Action<ILootBoxData, IItemData[]> LootBoxOpened;

        public void TriggerDummyLotBoxOpened()
        {
            LootBoxOpened?.Invoke(openEventLootBox, openEventItems);
        }
    }
}