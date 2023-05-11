using Inventory;

namespace Stat
{
    public class PlayerStats : IPlayerStats
    {
        public PlayerStats(IActiveInventory activeInventory)
        {
            ActiveInventory = activeInventory;
        }

        public IActiveInventory ActiveInventory { get; }

        public Stats GetCurrentStats()
        {
            var stats = new Stats();
            foreach (var item in ActiveInventory.EquippedItems)
            {
                stats = stats.Add(item.BonusStats);
                stats = stats.Add(item.ItemConfig.BaseStats);
            }

            return stats;
        }
    }
}