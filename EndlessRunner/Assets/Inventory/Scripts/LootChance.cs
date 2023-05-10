using System;
using Item;

namespace Inventory.Scripts
{
    [Serializable] public struct LootChance
    {
        public IItemConfig itemConfig;
        public int chance; // weight
    }
}
