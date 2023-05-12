using System.Linq;
using UnityEngine;

namespace Inventory.Scripts
{
    [CreateAssetMenu(fileName = "LootBoxDataBase", menuName = "LootBoxDataBase", order = 0)]
    public class LootBoxDataBaseSo : ScriptableObject
    {
        [SerializeField] private LootBoxConfigSo[] _lootBoxConfigs;

        public LootBoxConfigSo GetWithId(string id)
        {
            return _lootBoxConfigs.First(lootBoxConfig => lootBoxConfig.Id == id);
        }
    }
}
