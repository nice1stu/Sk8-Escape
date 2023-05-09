using Stat;

namespace Item
{
    public interface IItemData
    {
        IItemConfig ItemConfig { get; }
        //actual implementation of the bonus stats
        IStats BonusStats { get; }
    }
}