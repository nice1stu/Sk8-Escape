using System;
using System.Collections.Generic;
using System.Linq;
using Item;
using UnityEngine;

namespace Inventory
{
    [Serializable]
    public class DummyInventory : IInventoryData, IActiveInventory
    {
        [SerializeField] private ItemData dummyItemTemplate;
        [SerializeField] private List<ItemData> items;
        [SerializeField] private int equippedItemIndex;

        public IEnumerable<IItemData> EquippedItems
        {
            get { yield return items[equippedItemIndex]; }
        }

        public void Equip(IItemData item)
        {
            if (item is ItemData itemData)
            {
                ItemUnequipped?.Invoke(EquippedItems.First());
                equippedItemIndex = items.IndexOf(itemData);
                ItemEquipped?.Invoke(item);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public event Action<IItemData> ItemUnequipped;
        public event Action<IItemData> ItemEquipped;

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