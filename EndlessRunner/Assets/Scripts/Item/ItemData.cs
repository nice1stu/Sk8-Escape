using System;
using Stat;
using UnityEngine;

namespace Item
{
    [Serializable]
    public class ItemData : IItemData
    {
        [SerializeField] private Stats bonusStats;
        [SerializeField] private ItemConfigSO itemConfig;

        public ItemData(Stats bonusStats, ItemConfigSO itemConfig)
        {
            this.bonusStats = bonusStats;
            this.itemConfig = itemConfig;
        }

        public IItemConfig ItemConfig => itemConfig;

        public IStats BonusStats => bonusStats;

        public IStats TotalStats => itemConfig.BaseStats.Add(bonusStats);
    }
}