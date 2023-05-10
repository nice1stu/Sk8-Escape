using Inventory;
using Item;

public class ItemFactory : IItemFactory
{
    public IInventoryData Inventory { get; }
    public void CreateItem(IItemConfig itemConfig)
    {
    }
    
}
