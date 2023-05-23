using UnityEngine;
using TMPro;

public class PopupWindow : MonoBehaviour
{
    public GameObject popupWindowMessage; // Reference to the popup window game object
    public TextMeshProUGUI messageText; // Reference to the text component displaying the message
    public GameObject popupWindowConfirmation; // Reference to the popup window game object
    public TextMeshProUGUI confiramtionText; // Reference to the text component displaying the message
    private ItemShop shop;
    
    // Method to display the popup window with the given message
    private void Awake()
    {
        shop = GetComponent<ItemShop>();
    }

    public void ShowPopupMessage(string message)
    {
        // Enable the popup window game object
        popupWindowMessage.SetActive(true);
        // Set the message text
        messageText.text = message;
    }
    
    // Method to hide the popup window
    public void HidePopupMessage()
    {
        // Disable the popup window game object
        popupWindowMessage.SetActive(false);
    }
    
    public void ShowPopupConfirmation(string message)
    {
        // Enable the popup window game object
        popupWindowConfirmation.SetActive(true);
        // Set the message text
        confiramtionText.text = message;
        shop.LoadPanel();
    }
    
    // Method to hide the popup window
    public void HidePopupConfirmation()
    {
        // Disable the popup window game object
        popupWindowConfirmation.SetActive(false);   
    }

    public void confirmationSuccess()
    { // Disable the popup window game object
        popupWindowConfirmation.SetActive(false);
        shop.CheckPurchase();
        shop.DisablePanel();
        // ShowPopupMessage("Purchase successful");
    }
    
}

