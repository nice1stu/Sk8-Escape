using System.Collections.Generic;
using System.IO;
using System.Linq;
using Inventory;
using Item;
using UnityEngine;

public class InventorySerializer
{
    [SerializeField] private DummyInventory dummyInventory = new DummyInventory();
    
    private void OnEnable()
    {
        dummyInventory.ItemAdded += Save;
    }

    private void OnDisable()
    {
        dummyInventory.ItemAdded -= Save;
    }
    
    private void Save(IItemData itemData)
    {
        List<IItemData> data = dummyInventory.Items.ToList();
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/inventory.save.json", json);
    }
    [ContextMenu("Load")]
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
}
