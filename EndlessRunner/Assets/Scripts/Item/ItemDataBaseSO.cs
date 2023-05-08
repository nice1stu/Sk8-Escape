using System.Collections.Generic;
using System.Linq;
using Stat;
using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "ItemDataBase", menuName = "ItemDataBase", order = 0)]
    public class ItemDataBaseSO : ScriptableObject
    {
        [SerializeField] private ItemConfigSO[] itemConfigs;
        public IEnumerable<ItemConfigSO> ItemConfigs=> itemConfigs;

        public ItemConfigSO GetWithID(string id)
        {
            return itemConfigs.First(it => it.Id == id);
        }
    }
}