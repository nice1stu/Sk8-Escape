using System;
using System.Collections.Generic;
using System.Linq;
using Item;

namespace Inventory
{
    //The actual implementation of the inventory
    [Serializable]
    public class PlayerInventory : EquippedItemsInventory,IInventoryData
    {
        private IList<IItemData> _items = new List<IItemData>();
        public IEnumerable<IItemData> Items => _items;
        public void AddItem(IItemData item)
        {
            _items.Add(item);
            ItemAdded?.Invoke(item);
        }
        public void Load(IEnumerable<IItemData> items, int[] indices){
            _items = items.ToList();
            LoadEquip(items.ToArray(),indices);
        }
        public event Action<IItemData> ItemAdded;
    }
}
