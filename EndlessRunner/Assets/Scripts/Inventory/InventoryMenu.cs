using Item;
using UnityEngine;

namespace Inventory
{
    public class InventoryMenu : MonoBehaviour
    {
        public Transform SkateboardContent;

        public ItemPreview itemPrefab;
        // Start is called before the first frame update
        void Start()
        {
            foreach (var inventoryItem in Dependencies.Instance.Inventory.Items)
            {
                Instantiate(itemPrefab, SkateboardContent).Setup(inventoryItem);
            }
            Dependencies.Instance.Inventory.ItemAdded+= InventoryOnItemAdded;
        }

        private void InventoryOnItemAdded(IItemData obj)
        {
            Instantiate(itemPrefab, SkateboardContent).Setup(obj);
        }

        // Update is called once per frame
        void OnDestroy()
        {
            Dependencies.Instance.Inventory.ItemAdded-= InventoryOnItemAdded;
        }
    }
}
