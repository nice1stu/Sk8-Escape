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

        public IItemConfig ItemConfig => itemConfig;

        public IStats BonusStats => bonusStats;
    }
}