using UnityEngine;
using UnityEngine.UI;

namespace Inventory.Scripts
{
    public class InventorySlot : MonoBehaviour
    {
        public void AddLootBoxIcon(BaseLootBox lootBox)
        {
           var slotIcon = GetComponentInChildren<Image>();
           slotIcon.sprite = lootBox.icon;
        }
    }
}
