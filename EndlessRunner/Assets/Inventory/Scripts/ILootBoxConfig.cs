using System;
using UnityEngine;

namespace Inventory.Scripts
{
    public interface ILootBoxConfig
    {
        Sprite Icon { get; }
        string LootBoxName { get; }
        string Id { get; }
        TimeSpan TimeToOpen { get; }
        LootChance[] LootChances { get; }
        
    }
}
