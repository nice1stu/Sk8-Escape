namespace Inventory_Interfaces
{
    public interface IItemData
    {
        IItemConfig ItemConfig { get; set; }
        IStats BonusStats { get; set; }
    }
}