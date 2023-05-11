using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Item;
using Stat;
using UnityEngine;
using UnityEngine.Serialization;

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
            return new ItemData(serializableItemData.bonusStats,
                _itemDataBase.GetWithID(serializableItemData.itemConfigID));
        }

        private Stats Convert(IStats stats)
        {
            return new Stats
            {
                CoffinTimeAdded = stats.CoffinTimeAdded,
                Stability = stats.Stability,
                Vision = stats.Vision,
                GrindMiniGameBallSize = stats.GrindMiniGameBallSize,
                GrindLeniency = stats.GrindLeniency,
                ScoreMultiplier = stats.ScoreMultiplier
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
            return data.serializableItemDatas.Select(Convert).ToList();
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
            public string itemConfigID; 
            public Stats bonusStats;

            public SerializableItemData(string itemConfigID, Stats bonusStats)
            {
                this.itemConfigID = itemConfigID;
                this.bonusStats = bonusStats;
            }
        }

        [Serializable]
        private class SerializableInventory
        {
            public List<SerializableItemData> serializableItemDatas;

            public SerializableInventory(List<SerializableItemData> serializableItemDatas)
            {
                this.serializableItemDatas = serializableItemDatas;
            }
        }
    }
}