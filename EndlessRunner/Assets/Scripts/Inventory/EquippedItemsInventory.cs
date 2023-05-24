using System;
using System.Collections.Generic;
using System.Linq;
using Item;

namespace Inventory
{
    public class EquippedItemsInventory : IActiveInventory
    {
        private List<IItemData> _equippedItems = new List<IItemData>();
        public IEnumerable<IItemData> EquippedItems => _equippedItems;

        public void Equip(IItemData item)
        {
            foreach (var equippedItem in _equippedItems.Where(equippedItem =>
                         item.ItemConfig.ItemType == equippedItem.ItemConfig.ItemType))
            {
                _equippedItems.Add(item);
                _equippedItems.Remove(equippedItem);
                ItemUnequipped?.Invoke(equippedItem);
                ItemEquipped?.Invoke(item);
                return;
            }

            _equippedItems.Add(item);
            ItemEquipped?.Invoke(item);
        }

        protected void LoadEquip(IItemData[] items, int[] indices)
        {
            var itemList = new List<IItemData>();
            bool hasSkateboard = false;
            foreach (var t in indices)
            {
                itemList.Add(items[t]);
                if (items[t].ItemConfig.ItemType == ItemType.SkateBoard)
                {
                    hasSkateboard = true;
                } 
            }

            if (!hasSkateboard)
            {
                itemList.Add(items[0]);
            }
            
            _equippedItems = itemList;
        }
        

        public event Action<IItemData> ItemUnequipped;
        public event Action<IItemData> ItemEquipped;
    }
}