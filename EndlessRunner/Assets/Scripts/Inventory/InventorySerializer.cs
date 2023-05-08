using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Item;
using UnityEngine;

namespace Inventory
{
    public class InventorySerializer : IDisposable
    {
        private readonly IInventoryData _inventory;
        
        public InventorySerializer(IInventoryData inventory)
        {
            _inventory = inventory;
            _inventory.ItemAdded += Save;
        }
        
        private void Save(IItemData itemData)
        {
            List<IItemData> data = _inventory.Items.ToList();
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.persistentDataPath + "/inventory.save.json", json);
        }
        public static List<ItemData> Load()
        {
            //do I make fields of dummyInventory public or do I call this function from dummyInventory?
            string path = Application.persistentDataPath + "/inventory.save.json";
            if (!File.Exists(path)) return null;

            string json = File.ReadAllText(path);
            List<IItemData> data = JsonUtility.FromJson<List<IItemData>>(json);
            List<ItemData> itemDataList = new List<ItemData>();
            foreach (var itemData in data)
            {
                if(itemData is ItemData itemdata)
                    itemDataList.Add(itemdata);
            }
            return itemDataList;
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

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }
    }
}
