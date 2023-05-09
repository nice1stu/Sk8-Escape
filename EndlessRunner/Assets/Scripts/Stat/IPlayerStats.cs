using System;
using Inventory;
using Stat;
using UnityEngine;

namespace Stat
{
    public interface IPlayerStats
    {
        IActiveInventory ActiveInventory { get; }

        Stats GetCurrentStats()
        {
            var stats = new Stats();
            foreach (var item in ActiveInventory.EquippedItems)
            {
                stats = stats.Add(item.BonusStats);
                stats = stats.Add(item.ItemConfig.BaseStats);
            }
            return stats;
        }
    }
}