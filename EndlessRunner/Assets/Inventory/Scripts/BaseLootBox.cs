using System;
using UnityEngine;

namespace Inventory.Scripts
{
    public abstract class BaseLootBox : MonoBehaviour
    {
        // the image for the lootBox
        public abstract Sprite Icon { get; set; }
        
        // the time it takes for this type of loot box to open
        public abstract TimeSpan TimeToOpen { get; set; } 

        // what happens when the loot box is opened (gacha rates and give item)
        public abstract void OpenLootBox();
    }
}
