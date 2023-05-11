using System;
using System.IO;
using UnityEngine;

namespace Inventory.Scripts
{
    public class LootBoxSerializer : IDisposable
    {
        private readonly ILootBoxInventory _inventory;
        private readonly LootBoxData _lootBoxData;

        public LootBoxSerializer(ILootBoxInventory inventory, LootBoxData lootBoxData)
        {
            _inventory = inventory;
            _lootBoxData = lootBoxData;
            _inventory.LootBoxAdded += Save;
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }
        
        private SerializableLootBoxData Convert(ILootBoxData itemData)
        {
            return new SerializableLootBoxData(itemData.Config, itemData.OpeningStartTime);
        }

        private LootBoxData[] Convert(ILootBoxData[] lootBoxes)
        {
            var lootBoxDatas = new LootBoxData[4];
            for (var i = 0; i<lootBoxDatas.Length; i++)
            {
                lootBoxDatas[i] = new LootBoxData(lootBoxes[i].Config, lootBoxes[i].OpeningStartTime);
            }
            return lootBoxDatas;
        }

        private void Save(int index, ILootBoxData lootBoxData)
        {
            var json = JsonUtility.ToJson(Convert(_lootBoxData));
            File.WriteAllText(Application.persistentDataPath + "/lootBoxInventory.save.json", json);
        }

        public ILootBoxData[] Load()
        {
            //do I make fields of dummyInventory public or do I call this function from dummyInventory?
            var path = Application.persistentDataPath + "/lootBoxInventory.save.json";
            if (!File.Exists(path)) return null;

            var json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<LootBoxData[]>(json);
            return data;
        }

        ~LootBoxSerializer()
        {
            ReleaseUnmanagedResources();
        }

        private void ReleaseUnmanagedResources()
        {
            // TODO release unmanaged resources here
            _inventory.LootBoxAdded -= Save;
        }

        [Serializable] class SerializableLootBoxData
        {
            public readonly ILootBoxConfig config;
            public readonly DateTime openingStartTime;

            public SerializableLootBoxData(ILootBoxConfig config, DateTime openingStartTime)
            {
                this.config = config;
                this.openingStartTime = openingStartTime;
            }
        }
    }
}