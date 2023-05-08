using Inventory;
using Stat;

public interface IPlayerStats
{
    IActiveInventory activeInventory { get; }

    IStats GetCurrentStats()
    {
        var stats = new Stats();
        foreach (var item in activeInventory.EquippedItems)
        {
            // stats += item.BonusStats;
            // stats += item.ItemConfig.BaseStats;
        }

        return stats;
    }
}