using Inventory;

namespace Stat
{
    public interface IPlayerStats
    {
        IActiveInventory ActiveInventory { get; }

        Stats GetCurrentStats();

    }
}