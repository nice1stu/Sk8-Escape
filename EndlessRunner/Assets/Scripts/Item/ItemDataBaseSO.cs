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
        private IEnumerable<ItemConfigSO> ItemConfigs=> itemConfigs;

        public ItemConfigSO GetWithID(string id)
        {
            return ItemConfigs.First(it => it.Id == id);
        }
    }
}