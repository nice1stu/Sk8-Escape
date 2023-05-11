using Inventory;
using Stat;

namespace Item
{
    public interface IItemFactory
    {
        IInventoryData Inventory { get; }

        void CreateItem(ItemConfigSO itemConfig);
    }
}
