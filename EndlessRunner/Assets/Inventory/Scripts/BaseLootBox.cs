using UnityEngine;

namespace Inventory.Scripts
{
    public class BaseLootBox : MonoBehaviour
    {
        public Sprite icon; 
        
        //being able to open the loot box
       public void OpenLootBox()
       {
           BaseItem.coins++;
       }
    }
}
