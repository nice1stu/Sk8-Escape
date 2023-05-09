using Stat;

namespace Item
{
    public interface IItemData
    {
        IItemConfig ItemConfig { get; }
        IStats BonusStats { get; }
        IStats TotalStats { get; }
    }
}