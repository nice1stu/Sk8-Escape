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
        [SerializeField] private Color color;
        [SerializeField] private Sprite itemBorder;

        // do a public method with the exact type (not IStats):
        public Stats BaseStats => baseStats;
        public ItemType ItemType => itemType;
        public int BonusStats
        {
            get => bonusStats;
            set => bonusStats = value;
        }

        public string ItemName => itemName;
        public Sprite ItemSprite => itemSprite;

        public Sprite ItemBorder => itemBorder;
        public Sprite ItemIcon => itemIcon;
        public string Id => id;

        public Color Color => color;

        // then, do an explicit interface implementation using the interface type:
        IStats IItemConfig.BaseStats => baseStats;
    }
}