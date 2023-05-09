using System;
using System.Collections.Generic;
using Item;

namespace Inventory
{
    public interface IActiveInventory
    {
        IEnumerable<IItemData> EquippedItems { get; }

        void Equip(IItemData item);

        event Action<IItemData> ItemUnequipped;
        event Action<IItemData> ItemEquipped;
    }
}