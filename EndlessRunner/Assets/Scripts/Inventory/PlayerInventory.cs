using System;
using System.Collections.Generic;
using Item;

namespace Inventory
{
    //The actual implementation of the inventory
    [Serializable]
    public class PlayerInventory : EquippedItemsInventory,IInventoryData
    {
        private List<IItemData> _items = new ();
        public IEnumerable<IItemData> Items => _items;
        public void AddItem(IItemData item)
        {
            _items.Add(item);
            ItemAdded?.Invoke(item);
        }

        public event Action<IItemData> ItemAdded;
    }
}
