using System;
using Item;

namespace Inventory.Scripts
{
    [Serializable]
    public struct LootChance
    {
        public ItemConfigSO itemConfig;
        public int chance; // weight
    }
}
