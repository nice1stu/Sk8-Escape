using Inventory;
using Stat;

namespace Item
{
    public interface IItemFactory
    {
        IInventoryData Inventory { get; }

        ItemData CreateItem(IItemConfig itemConfig);
    }
}
