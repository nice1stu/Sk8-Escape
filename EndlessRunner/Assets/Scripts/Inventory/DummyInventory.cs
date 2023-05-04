using System;
using System.Collections.Generic;
using Item;
using UnityEngine;

namespace Inventory
{
    public class DummyInventory: IInventoryData
    {
        [SerializeField]private List<ItemData> items;

        public IEnumerable<IItemData> Items => items;

        public void AddItem(IItemData item)
        {
            throw new NotImplementedException();
        }

        public event Action<IItemData> ItemAdded;
    }
}