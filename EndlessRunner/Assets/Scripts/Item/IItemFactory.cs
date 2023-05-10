using Inventory;

namespace Item
{
    public interface IItemFactory
    {
        IInventoryData Inventory { get; }

        void CreateItem(IItemConfig itemConfig);
    }
}
