using Inventory;

public static class Dependencies
{
    public static IInventoryData Inventory { get; } = null;

    public static IActiveInventory Equipped { get; } = null;
    // public static IPlayerStats PlayerStats {get;}
    // public static IItemFactory ItemFactory {get;}
}