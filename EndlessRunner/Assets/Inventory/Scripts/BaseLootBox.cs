using UnityEngine;

namespace Inventory.Scripts
{
    public class BaseLootBox : MonoBehaviour
    {
        public Sprite icon;
        public Time timeToOpen;
        public int discardValue;
        
        //being able to open the loot box
       public void OpenLootBox()
       {
           BaseItem.coins++;
       }
    }
}
