using System;
using System.Collections.Generic;
using System.Linq;
using Item;

namespace Inventory
{
    public class EquippedItemsInventory : IActiveInventory
    {
        private List<IItemData> _equippedItems;
        public IEnumerable<IItemData> EquippedItems => _equippedItems;

        public void Equip(IItemData item)
        {
            foreach (var equippedItem in _equippedItems.Where(equippedItem =>
                         item.ItemConfig.ItemType == equippedItem.ItemConfig.ItemType))
            {
                ItemUnequipped?.Invoke(equippedItem);
                _equippedItems.Remove(equippedItem);
                ItemEquipped?.Invoke(item);
                _equippedItems.Add(item);
                return;
            }

            ItemEquipped?.Invoke(item);
            _equippedItems.Add(item);
        }

        public event Action<IItemData> ItemUnequipped;
        public event Action<IItemData> ItemEquipped;
    }
}