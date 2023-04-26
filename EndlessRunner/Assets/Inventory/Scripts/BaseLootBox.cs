using System;
using UnityEngine;

namespace Inventory.Scripts
{
    public class BaseLootBox : MonoBehaviour
    {
        public Sprite icon;
        public TimeSpan timeToOpen;

        private void Start()
        {
            timeToOpen = TimeSpan.FromSeconds(10);
        }

        //being able to open the loot box
       public void OpenLootBox()
       {
           BaseItem.coins++;
           Debug.Log($"Coins earned: {BaseItem.coins}");
       }
    }
}
