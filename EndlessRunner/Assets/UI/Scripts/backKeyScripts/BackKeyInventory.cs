using UnityEngine;
using UnityEngine.UI;

public class BackKeyInventory : MonoBehaviour
{
    public ItemDetailPopup itemPopUp;
    public Button PopUpBackButton;
    public Button InventoryBackButton;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (itemPopUp.gameObject.activeInHierarchy)
            {
                PopUpBackButton.onClick.Invoke();
            }
            else
            {
                InventoryBackButton.onClick.Invoke();
            }
        }
    }
}
