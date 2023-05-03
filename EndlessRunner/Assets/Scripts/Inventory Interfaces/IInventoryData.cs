using System;
using System.Collections.Generic;

namespace Inventory_Interfaces
{
    public interface IInventoryData
    {
        List<IItemData> Items {get;}

        void AddItem(IItemData item);

        event Action<IItemData> ItemAdded;
    }
}
