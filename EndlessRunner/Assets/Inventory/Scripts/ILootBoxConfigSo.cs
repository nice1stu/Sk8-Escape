using System;
using UnityEngine;

namespace Inventory.Scripts
{
    [CreateAssetMenu(fileName = "NewLootBoxConfig", menuName = "LootBoxConfig", order = 0)]
    public class LootBoxConfigSo : ScriptableObject, ILootBoxConfig
    {
        public Sprite Icon { get; }
        public string Name { get; }
        public string Id { get; }
        public TimeSpan TimeToOpen { get; }
        public LootChance[] LootChances { get; }
    }
}
