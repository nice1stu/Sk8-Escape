using System;
using System.Collections.Generic;
using System.Linq;
using Stat;
using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "ItemDataBase", menuName = "ItemDataBase", order = 0)]
    public class ItemDataBaseSO : ScriptableObject
    {
        [SerializeField] public ItemConfigSO[] itemConfigs;
        private IEnumerable<ItemConfigSO> ItemConfigs=> itemConfigs;
        public bool TryLoadItemWithID(string id, out ItemConfigSO result)
        {
            try
            {
                return result = ItemConfigs.First(it => it.Id == id);
            }
            catch (Exception e)
            {
                return result = null;
            }
        }
    }
}