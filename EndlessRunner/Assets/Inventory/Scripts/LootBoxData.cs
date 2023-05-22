using System;
using UnityEngine;

namespace Inventory.Scripts
{
    public class LootBoxData : ILootBoxData
    {
        private readonly ILootBoxConfig _config;
        private readonly DateTime _openingStartTime;

        public LootBoxData(ILootBoxConfig config, DateTime openingStartTime)
        {
            _config = config;
            _openingStartTime = openingStartTime;
        }
        
        public ILootBoxConfig Config => _config;

        public DateTime OpeningStartTime => _openingStartTime;
    }
}
