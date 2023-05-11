using System;
using UnityEngine;

namespace Inventory.Scripts
{
    [CreateAssetMenu(fileName = "NewLootBoxConfig", menuName = "LootBoxConfig", order = 0)]
    public class LootBoxConfigSo : ScriptableObject, ILootBoxConfig
    {
        [SerializeField] private Sprite icon;
        [SerializeField] private string lootBoxName;
        [SerializeField] private string id;
        [SerializeField] private int _minutesToOpen;
        [SerializeField] private LootChance[] lootChances;

        public Sprite Icon => icon;
        public string LootBoxName => lootBoxName;
        public string Id => id;
        public TimeSpan TimeToOpen => new TimeSpan(0, _minutesToOpen, 0);
        public LootChance[] LootChances => lootChances;
        
    }
}
