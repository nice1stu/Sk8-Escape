using System;
using System.Collections;
using System.Collections.Generic;
using Inventory;
using Inventory.Scripts;
using Item;
using UnityEngine;

public class LootBoxChanceTests : MonoBehaviour
{
    class DummyItemFactory : IItemFactory
    {
        public IInventoryData Inventory { get; }
        public IItemData CreateItem(IItemConfig itemConfig)
        {
            return new ItemData(default, itemConfig);
        }
    }
    
    public LootBoxConfigSo lootBoxToTest;

    private Dictionary<string, int> itemsReceived;
    // Start is called before the first frame update
    void Start()
    {
        var inventory = new LootBoxInventory(new DummyItemFactory());
        itemsReceived = new Dictionary<string, int>();
        inventory.LootBoxOpened += InventoryOnLootBoxOpened;
        for (int i = 0; i < 100000; i++)
        {
            var lootBox = new LootBoxData(lootBoxToTest, new DateTime(1970, 1, 1));
            inventory.AddLootBox(lootBox);
            inventory.OpenLootBox(lootBox);
        }
        inventory.LootBoxOpened -= InventoryOnLootBoxOpened;
        foreach (var pair in itemsReceived)
        {
            Debug.Log($"Received {pair.Key} {pair.Value} times.");
        }
        
    }

    private void InventoryOnLootBoxOpened(ILootBoxData arg1, IItemData[] arg2)
    {
        foreach (var item in arg2)
        {
            if(itemsReceived.ContainsKey(item.ItemConfig.Id))
            {
                itemsReceived[item.ItemConfig.Id]++;
            }
            else
            {
                itemsReceived[item.ItemConfig.Id] = 1;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
