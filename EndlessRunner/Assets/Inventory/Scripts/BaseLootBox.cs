using UnityEngine;

namespace Inventory.Scripts
{
    public class BaseLootBox : MonoBehaviour
    {
        //being able to open the loot box
       public void OpenLootBox()
       {
           BaseItem.coins++;
       }
    }
}
