using System;
using UnityEngine;

namespace Inventory.Scripts
{
    [CreateAssetMenu(fileName = "NewLootBoxConfig", menuName = "LootBoxConfig", order = 0)]
    public class LootBoxConfigSo : ScriptableObject, ILootBoxConfig
    {
        [SerializeField] private Sprite icon;
        [SerializeField] private Sprite boxOpen;
        [SerializeField] private string lootBoxName;
        [SerializeField] private string id;
        [SerializeField] private TimeSpan _timeToOpen;
        [SerializeField] private LootChance[] lootChances;

        public Sprite Icon => icon;
        public Sprite BoxOpen => boxOpen;
        public string LootBoxName => lootBoxName;
        public string Id => id;
        public TimeSpan TimeToOpen => _timeToOpen;
        public LootChance[] LootChances => lootChances;
        
    }
}
