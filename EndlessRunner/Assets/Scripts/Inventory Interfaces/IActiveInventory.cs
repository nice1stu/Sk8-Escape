using System;

namespace Inventory_Interfaces
{
    public interface IActiveInventory
    {
        IItemData[] EquippedItems { get; set; }

        void Equip(IItemData item);

        event Action<IItemData> ItemUnequipped;
        event Action<IItemData> ItemEquipped;
    }
}