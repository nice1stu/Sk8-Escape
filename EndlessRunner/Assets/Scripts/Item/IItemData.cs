using Stat;

namespace Item
{
    public interface IItemData
    {
        IItemConfig ItemConfig { get; set; }
        IStats BonusStats { get; set; }
    }
}