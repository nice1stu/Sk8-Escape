using System;
using UnityEngine;

namespace Inventory.Scripts
{
    public interface ILootBoxConfig
    {
        Sprite Icon { get; }
        String Name { get; }
        String Id { get; }
        TimeSpan TimeToOpen { get; }
        LootChance[] LootChances { get; }
        
    }
}
