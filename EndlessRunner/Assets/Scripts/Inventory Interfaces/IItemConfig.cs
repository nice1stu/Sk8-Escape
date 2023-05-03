using UnityEngine;

namespace Inventory_Interfaces
{
    public interface IItemConfig
    {
        IStats BaseStats { get; set; }
        ItemType ItemType { get; set; }
        int BonusStats { get; set; }
        string ItemName { get; set; }
        Sprite ItemSprite { get; set; }
        Sprite ItemIcon { get; set; }
        string ID { get; set; }
    }
}
