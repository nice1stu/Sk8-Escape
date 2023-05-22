using Inventory;
using Stat;

namespace Item
{
    public interface IItemFactory
    {
        IInventoryData Inventory { get; }

        IItemData CreateItem(IItemConfig itemConfig);
    }
}
