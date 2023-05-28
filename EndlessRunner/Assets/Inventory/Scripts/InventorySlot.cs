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
        public GameObject checklistsParent;

        private void Start()
        {
            _countdown ??= GetComponentInChildren<Countdown>();
            var currentState = Dependencies.Instance.LootBoxes.Slots[index];
            if (currentState != null)
            {
                AddLootBoxIcon(currentState);
            }
            Dependencies.Instance.LootBoxes.LootBoxAdded += LootBoxesOnLootBoxAdded;
            Dependencies.Instance.LootBoxes.LootBoxRemoved += LootBoxesOnLootBoxRemoved;
        }

        private void OnDestroy()
        {
            Dependencies.Instance.LootBoxes.LootBoxAdded -= LootBoxesOnLootBoxAdded;
            Dependencies.Instance.LootBoxes.LootBoxRemoved -= LootBoxesOnLootBoxRemoved;

        }

        private void LootBoxesOnLootBoxRemoved(int arg1, ILootBoxData arg2)
        {
            if (index != arg1) return;
            OnRemove(arg2);
        }

        private void LootBoxesOnLootBoxAdded(int arg1, ILootBoxData arg2)
        {
            if (index != arg1) return;
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
            slotIcon.enabled = true;
            
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
            _countdown.StopCountDown();
            slotIcon.sprite = null;//Removes the image
            slotIcon.enabled = false;
            Dependencies.Instance.LootBoxes.OpenLootBox(lootBox);//Calls the function to remove from the loot box inventory
        }

        public void OpenLootBoxFromBtnClick(int btnNo)
        {
            if (Dependencies.Instance.LootBoxes.Slots[btnNo] == null) return;
            // If slot is not null it checks if timer has finished. Little hacky but it works, would be great to have timer on the lootbox itself
            var t = Dependencies.Instance.LootBoxes.Slots[btnNo];
            var timeFromStart = DateTime.UtcNow - t.OpeningStartTime;
            // if the time the loot box takes to open has passed we open it
            if (timeFromStart >= t.Config.TimeToOpen) 
            {
                //Opens the lootBox on clicked btnNo Slot
                Dependencies.Instance.LootBoxes.OpenLootBox(Dependencies.Instance.LootBoxes.Slots[btnNo]);
                // extremely ugly hack to remove the checkmark when clicked(since there are no reference to the slots pls fix someone
                checklistsParent.transform.GetChild(btnNo+1).GetChild(0).GetChild(1).gameObject.SetActive(false);
            }
        }
    }
}
