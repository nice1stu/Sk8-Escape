using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Inventory.Scripts
{
    public class LootBoxSerializer : IDisposable
    {
        private readonly ILootBoxInventory _inventory;
        private readonly LootBoxDataBaseSo _lootBoxDataBase;

        public LootBoxSerializer(ILootBoxInventory inventory, LootBoxDataBaseSo lootBoxDataBase)
        {
            _inventory = inventory;
            _lootBoxDataBase = lootBoxDataBase;
            _inventory.LootBoxAdded += Save;
            _inventory.LootBoxRemoved += Save;
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }
        
        private SerializableLootBoxData Convert(ILootBoxData itemData)
        {
            if (itemData == null) return new SerializableLootBoxData();
            return new SerializableLootBoxData(itemData.Config.Id, itemData.OpeningStartTime);
        }

        private LootBoxData Convert(SerializableLootBoxData lootBox)
        {
            if (string.IsNullOrEmpty(lootBox.lootBoxId)) return null;
            return new LootBoxData(_lootBoxDataBase.GetWithId(lootBox.lootBoxId), new DateTime(1970, 1, 1).AddSeconds(lootBox.openingStartTime));
        }

        private void Save(int index, ILootBoxData lootBoxData)
        {
            var data = _inventory.Slots.Select(Convert).ToList();
            var lootBox = new SerializableLootBoxInventory(data);
            var json = JsonUtility.ToJson(lootBox);
            File.WriteAllText(Application.persistentDataPath + "/lootBoxInventory.save.json", json);
        }

        public IEnumerable<ILootBoxData> Load()
        {
            var path = Application.persistentDataPath + "/lootBoxInventory.save.json";
            if (!File.Exists(path)) return Enumerable.Repeat(default(ILootBoxData), 4);

            var json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<SerializableLootBoxInventory>(json);
            return data.serializableLootBoxData.Select(Convert).ToArray();
        }

        ~LootBoxSerializer()
        {
            ReleaseUnmanagedResources();
        }

        private void ReleaseUnmanagedResources()
        {
            // TODO release unmanaged resources here
            _inventory.LootBoxAdded -= Save;
            _inventory.LootBoxRemoved -= Save;
        }

        [Serializable] public class SerializableLootBoxData
        {
            public string lootBoxId;
            public int openingStartTime;

            public SerializableLootBoxData()
            {
                
            }
            public SerializableLootBoxData(string lootBoxId, DateTime openingStartTime)
            {
                this.lootBoxId = lootBoxId;
                this.openingStartTime = (int)openingStartTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            }
        }
        
        [Serializable]
        private class SerializableLootBoxInventory
        {
            public List<SerializableLootBoxData> serializableLootBoxData;
            
            public SerializableLootBoxInventory(List<LootBoxSerializer.SerializableLootBoxData> serializableLootBoxData)
            {
                this.serializableLootBoxData = serializableLootBoxData;
            }
        }
    }
}