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
            foreach (var inventoryItem in Dependencies.Inventory.Items)
            {
                Instantiate(itemPrefab, SkateboardContent);
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
