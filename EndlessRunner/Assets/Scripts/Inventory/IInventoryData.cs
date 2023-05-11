using System;
using System.Collections.Generic;
using Item;

namespace Inventory
{
    public interface IInventoryData
    {
        IEnumerable<IItemData> Items { get; }

        void AddItem(IItemData item);

        event Action<IItemData> ItemAdded;
    }
}