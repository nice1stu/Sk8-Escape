using System;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.Scripts
{
    public class InventorySlot : MonoBehaviour
    {
        //Index for the inventory slot
        [SerializeField] private int index;
        //Countdown for the inventory slot
        private Countdown _countdown;

        private void Start()
        {
            _countdown = gameObject.AddComponent<Countdown>();//adds the countdown script to the inventory slot gameObject
        }
        
        public void AddLootBoxIcon(BaseLootBox lootBox)//Function to set the loot box icon on the inventory slot
        {
            var childrenToLootBoxItem = gameObject.GetComponentsInChildren<Image>();//Gets all children with an image component
            var slotIcon = FindObjectOfType<Image>(); //Finds image so that it wont be null (we did this to make rider stop complaining)
            foreach (var child in childrenToLootBoxItem)
            {
                if (child.gameObject.CompareTag("LootBoxIcon"))//Check if the child has the tag
                {
                    slotIcon = child;//If it's true, set slot icon to child
                }
            }
            slotIcon.sprite = lootBox.icon;//Sets the slot icon sprite to the loot box image
            _countdown.StartCountdown(lootBox);//Start countdown
        }

        //May be removed in a later stage, good for testing
        [ContextMenu("Remove Loot Box")]
        void Remove()//This function is for the context menu
        {
            OnRemove();
        }
        
        public void OnRemove()//This function is supposed to be called on the discard button
        {
            //This part is the same as the AddLootBoxIcon function
            var childrenToLootBoxItem = gameObject.GetComponentsInChildren<Image>();
            var slotIcon = FindObjectOfType<Image>(); 
            foreach (var child in childrenToLootBoxItem)
            {
                if (child.gameObject.CompareTag("LootBoxIcon"))
                {
                    slotIcon = child;
                }
            }
            
            slotIcon.sprite = null;//Removes the image
            LootBoxInventory.RemoveLootBox(index);//Calls the function to remove from the loot box inventory
        }
    }
}
