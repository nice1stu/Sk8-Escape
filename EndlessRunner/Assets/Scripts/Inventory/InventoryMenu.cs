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
                Instantiate(itemPrefab, SkateboardContent);
            }
            Dependencies.Instance.Inventory.ItemAdded+= InventoryOnItemAdded;
        }

        private void InventoryOnItemAdded(IItemData obj)
        {
            Instantiate(itemPrefab, SkateboardContent);
        }

        // Update is called once per frame
        void OnDestroy()
        {
            Dependencies.Instance.Inventory.ItemAdded-= InventoryOnItemAdded;
        }
    }
}
