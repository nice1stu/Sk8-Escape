using Stat;
using UnityEngine;

namespace Item
{
    public interface IItemConfig
    {
        IStats BaseStats { get; }
        ItemType ItemType { get; }
        //amount of BonusStats
        int BonusStats { get; set; }
        string ItemName { get; }
        Sprite ItemSprite { get; }
        Sprite ItemIcon { get; }
        Sprite ItemBorder { get; }
        string Id { get; }
        
        Color Color { get; }
    }
}