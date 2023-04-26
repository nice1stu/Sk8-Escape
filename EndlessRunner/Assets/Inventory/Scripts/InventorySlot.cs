using UnityEngine;
using UnityEngine.UI;

namespace Inventory.Scripts
{
    public class InventorySlot : MonoBehaviour
    {
        [SerializeField] private int index;
        public void AddLootBoxIcon(BaseLootBox lootBox)
        {
            var slotImage = GameObject.FindGameObjectWithTag("LootBoxIcon");
            var slotIcon = slotImage.GetComponent<Image>();
            slotIcon.sprite = lootBox.icon;
        }

        [ContextMenu("Remove Loot Box")]
        void Remove()
        {
            OnRemove();
        }
        
        public void OnRemove()
        {
            var slotIcon = GetComponentInChildren<Image>();
            slotIcon.sprite = null;
            LootBoxInventory.RemoveLootBox(index);
        }
    }
}
