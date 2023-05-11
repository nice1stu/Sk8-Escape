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
        [SerializeField] private Countdown _countdown;
       

        private void Start()
        {
            var currentState = Dependencies.Instance.LootBoxes.Slots[index];
            Dependencies.Instance.LootBoxes.LootBoxAdded += LootBoxesOnLootBoxAdded;
            Dependencies.Instance.LootBoxes.LootBoxRemoved += LootBoxesOnLootBoxRemoved;
            _countdown ??= GetComponentInChildren<Countdown>();
        }

        private void OnDestroy()
        {
            Dependencies.Instance.LootBoxes.LootBoxAdded -= LootBoxesOnLootBoxAdded;
            Dependencies.Instance.LootBoxes.LootBoxRemoved -= LootBoxesOnLootBoxRemoved;

        }

        private void LootBoxesOnLootBoxRemoved(int arg1, ILootBoxData arg2)
        {
            if (index != arg1) return;
            _countdown.StopCountDown();
            OnRemove(arg2);
        }

        private void LootBoxesOnLootBoxAdded(int arg1, ILootBoxData arg2)
        {
            if (index != arg1) return;
            _countdown.StartCountdown(arg2);
            AddLootBoxIcon(arg2);
        }

        private void AddLootBoxIcon(ILootBoxData lootBox)//Function to set the loot box icon on the inventory slot
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
            slotIcon.sprite = lootBox.Config.Icon;//Sets the slot icon sprite to the loot box image
            _countdown.StartCountdown(lootBox);//Start countdown
        }

        private void OnRemove(ILootBoxData lootBox)//This function is supposed to be called on the discard button
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
            Dependencies.Instance.LootBoxes.OpenLootBox(lootBox);//Calls the function to remove from the loot box inventory
        }
    }
}
