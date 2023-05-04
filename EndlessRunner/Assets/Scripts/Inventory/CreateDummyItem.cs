using UnityEngine;

namespace Inventory
{
    public class CreateDummyItem : MonoBehaviour
    {
        public void OnClick()
        {
            (Dependencies.Instance.Inventory as DummyInventory).CreateDummyItem();
        }
    }
}
