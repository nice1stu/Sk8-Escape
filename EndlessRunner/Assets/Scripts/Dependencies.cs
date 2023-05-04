using Inventory;

public static class Dependencies
{
    private static readonly IInventoryData Inventory1 = null;

    public static IInventoryData Inventory => Inventory1;

    public static IActiveInventory Equipped { get; } = null;
    // public static IPlayerStats PlayerStats {get;}
    // public static IItemFactory ItemFactory {get;}
}