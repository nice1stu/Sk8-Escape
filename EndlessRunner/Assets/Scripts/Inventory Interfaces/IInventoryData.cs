using System;
using System.Collections.Generic;
using Inventory_Interfaces;
using UnityEngine;

namespace Inventory_Interfaces
{
    public interface IInventoryData
    {
        IEnumerable<IItemData> Items {get;}

        void AddItem(IItemData item);

        event Action<IItemData> ItemAdded;
    }
}