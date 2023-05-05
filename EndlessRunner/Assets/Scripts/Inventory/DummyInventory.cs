using System;
using System.Collections.Generic;
using Item;
using UnityEngine;

namespace Inventory
{
    [Serializable]
    public class DummyInventory: IInventoryData
    {
        [SerializeField] private ItemData dummyItemTemplate;
        [SerializeField]private List<ItemData> items;

        public IEnumerable<IItemData> Items => items;

        [ContextMenu("CreateDummyItem")]
        public void CreateDummyItem()
        {
            AddItem(dummyItemTemplate);
        }
        
        public void AddItem(IItemData item)
        {
            if (item is ItemData itemData)
            {
                items.Add(itemData);
                ItemAdded?.Invoke(itemData);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public event Action<IItemData> ItemAdded;
    }
}