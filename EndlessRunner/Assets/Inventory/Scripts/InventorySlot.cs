using System;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.Scripts
{
    public class InventorySlot : MonoBehaviour
    {
        [SerializeField] private int index;
        private Countdown _countdown;

        private void Start()
        {
            _countdown = gameObject.AddComponent<Countdown>();
        }

        public void AddLootBoxIcon(BaseLootBox lootBox)
        {
            var childrenToLootBoxItem = gameObject.GetComponentsInChildren<Image>();
            var slotIcon = FindObjectOfType<Image>(); 
            foreach (var child in childrenToLootBoxItem)
            {
                if (child.gameObject.CompareTag("LootBoxIcon"))
                {
                    slotIcon = child;
                }
            }
            slotIcon.sprite = lootBox.icon;
            _countdown.StartCountdown(lootBox);
        }

        [ContextMenu("Remove Loot Box")]
        void Remove()
        {
            OnRemove();
        }
        
        public void OnRemove()
        {
            var childrenToLootBoxItem = gameObject.GetComponentsInChildren<Image>();
            var slotIcon = FindObjectOfType<Image>(); 
            foreach (var child in childrenToLootBoxItem)
            {
                if (child.gameObject.CompareTag("LootBoxIcon"))
                {
                    slotIcon = child;
                }
            }
            
            slotIcon.sprite = null;
            LootBoxInventory.RemoveLootBox(index);
        }
    }
}
