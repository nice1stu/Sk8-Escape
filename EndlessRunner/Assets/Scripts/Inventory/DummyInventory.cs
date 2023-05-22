using System;
using System.Collections.Generic;
using System.Linq;
using Item;
using UnityEngine;

namespace Inventory
{
    [Serializable]
    public class DummyInventory : IInventoryData
    {
        [SerializeField] private ItemData dummyItemTemplate;
        [SerializeField] private List<ItemData> items;
        [SerializeField] private int equippedItemIndex;
        public readonly EquippedItemsInventory equippedItems = new EquippedItemsInventory();

        public IEnumerable<IItemData> Items => items;

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

        [ContextMenu("CreateDummyItem")]
        public void CreateDummyItem()
        {
            AddItem(dummyItemTemplate);
        }

        public void Load(List<ItemData> itemDatas)
        {
            items = itemDatas;
        }
    }
}