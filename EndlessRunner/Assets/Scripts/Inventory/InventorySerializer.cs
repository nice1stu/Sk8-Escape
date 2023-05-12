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
        private readonly IActiveInventory _activeInventory;
        private readonly ItemDataBaseSO _itemDataBase;

        public InventorySerializer(IInventoryData inventory, ItemDataBaseSO itemDataBase, IActiveInventory activeInventory)
        {
            _inventory = inventory;
            _itemDataBase = itemDataBase;
            _inventory.ItemAdded += Save;
            _activeInventory = activeInventory;
            _activeInventory.ItemEquipped += SaveEquip;
            _activeInventory.ItemUnequipped += SaveEquip;
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

        class SeralizableIntArray
        {
            public int[] array;

            public SeralizableIntArray(int[] array)
            {
                this.array = array;
            }
        }
        private void Save(IItemData itemData)
        {
            var data = _inventory.Items.Select(Convert).ToList();
            var inventory = new SerializableInventory(data);
            var json = JsonUtility.ToJson(inventory);
            File.WriteAllText(Application.persistentDataPath + "/inventory.save.json", json);
        }
        
        private void SaveEquip(IItemData itemData)
        {
            var indices = new int[_activeInventory.EquippedItems.ToArray().Length];
            var i = 0;
            foreach (var inventoryItem in _inventory.Items)
            {
                foreach (var activeInventoryEquippedItem in _activeInventory.EquippedItems)
                {
                    if (inventoryItem == activeInventoryEquippedItem)
                    {
                        var index = Array.IndexOf(_inventory.Items.ToArray(), inventoryItem);
                        indices[i] = index;
                        i++;
                    }
                }
            }
            
            var json = JsonUtility.ToJson(new SeralizableIntArray(indices));
            File.WriteAllText(Application.persistentDataPath + "/equip.save.json", json);
        }
        public int[] LoadEquip()
        {
            var path = Application.persistentDataPath + "/equip.save.json";
            if (!File.Exists(path)) return Array.Empty<int>();

            var json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<SeralizableIntArray>(json);
            return data.array;
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
            _activeInventory.ItemEquipped -= SaveEquip;
            _activeInventory.ItemUnequipped -= SaveEquip;
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