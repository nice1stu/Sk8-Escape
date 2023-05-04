using Stat;
using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "NewItemConfig", menuName = "ItemConfig", order = 0)]
    public class ItemConfigSO : ScriptableObject, IItemConfig
    {
        [SerializeField] private Stats baseStats;
        [SerializeField] private ItemType itemType;
        [SerializeField] private int bonusStats;
        [SerializeField] private string itemName;
        [SerializeField] private Sprite itemSprite;
        [SerializeField] private Sprite itemIcon;
        [SerializeField] private string id;

        // do a public method with the exact type (not IStats):
        public Stats BaseStats => baseStats;
        public ItemType ItemType => itemType;
        public int BonusStats => bonusStats;
        public string ItemName => itemName;
        public Sprite ItemSprite => itemSprite;
        public Sprite ItemIcon => itemIcon;
        public string Id => id;

        // then, do an explicit interface implementation using the interface type:
        IStats IItemConfig.BaseStats => baseStats;
    }
}