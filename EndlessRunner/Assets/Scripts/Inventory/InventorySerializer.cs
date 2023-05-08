using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Item;
using Stat;
using UnityEngine;

namespace Inventory
{
    public class InventorySerializer : IDisposable
    {
        private readonly IInventoryData _inventory;
        private readonly ItemDataBaseSO _itemDataBase;

        public InventorySerializer(IInventoryData inventory, ItemDataBaseSO itemDataBase)
        {
            _inventory = inventory;
            _itemDataBase = itemDataBase;
            _inventory.ItemAdded += Save;
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        private SerializableItemData Convert(IItemData itemData)
        {
            return new SerializableItemData(itemData.ItemConfig.Id, Convert(itemData.BonusStats));
        }

        private ItemData Convert(SerializableItemData serializableItemData)
        {
            return new ItemData(serializableItemData.BonusStats,
                _itemDataBase.GetWithID(serializableItemData.ItemConfigID));
        }

        private Stats Convert(IStats stats)
        {
            return new Stats
            {
                Speed = stats.Speed,
                Stability = stats.Stability,
                Style = stats.Style,
                Balance = stats.Balance
            };
        }

        private void Save(IItemData itemData)
        {
            var data = _inventory.Items.Select(Convert).ToList();
            var inventory = new SerializableInventory(data);
            var json = JsonUtility.ToJson(inventory);
            File.WriteAllText(Application.persistentDataPath + "/inventory.save.json", json);
        }

        public List<ItemData> Load()
        {
            //do I make fields of dummyInventory public or do I call this function from dummyInventory?
            var path = Application.persistentDataPath + "/inventory.save.json";
            if (!File.Exists(path)) return null;

            var json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<SerializableInventory>(json);
            return data._serializableItemDatas.Select(Convert).ToList();
        }

        ~InventorySerializer()
        {
            ReleaseUnmanagedResources();
        }

        private void ReleaseUnmanagedResources()
        {
            // TODO release unmanaged resources here
            _inventory.ItemAdded -= Save;
        }

        [Serializable]
        public class SerializableItemData
        {
            public string ItemConfigID;
            public Stats BonusStats;

            public SerializableItemData(string itemConfigID, Stats bonusStats)
            {
                ItemConfigID = itemConfigID;
                BonusStats = bonusStats;
            }
        }

        [Serializable]
        private class SerializableInventory
        {
            public List<SerializableItemData> _serializableItemDatas;

            public SerializableInventory(List<SerializableItemData> serializableItemDatas)
            {
                _serializableItemDatas = serializableItemDatas;
            }
        }
    }
}