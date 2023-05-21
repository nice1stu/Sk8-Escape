using UnityEngine;
using TMPro;

public class PopupWindow : MonoBehaviour
{
    public GameObject popupWindowMessage; // Reference to the popup window game object
    public TextMeshProUGUI messageText; // Reference to the text component displaying the message
    public GameObject popupWindowConfirmation; // Reference to the popup window game object
    public GameObject gemWindowConfirmation; // Reference to the gem popup window game object
    public TextMeshProUGUI gemConfiramtionText; // Reference to the text component displaying the message
    public TextMeshProUGUI confiramtionText; // Reference to the text component displaying the message
    private ItemShop shop;
    private GemShop gempack;
    
    // Method to display the popup window with the given message
    private void Awake()
    {
        shop = GetComponent<ItemShop>();
        gempack = GetComponent<GemShop>();
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
    }
    
    // Method to hide the popup window
    public void HidePopupConfirmation()
    {
        // Disable the popup window game object
        popupWindowConfirmation.SetActive(false);
        shop.LoadPanel();
    }

    public void confirmationSuccess()
    { // Disable the popup window game object
        popupWindowConfirmation.SetActive(false);
        ShowPopupMessage("Purchase successful");
        shop.CheckPurchase();
    }
    
    public void GemPopupConfirmation(string message)
    {
        // Enable the popup window game object
        gemWindowConfirmation.SetActive(true);
        // Set the message text
        confiramtionText.text = message;
    }
    
    public void HideGemConfirmation()
    {
        // Disable the popup window game object
        gemWindowConfirmation.SetActive(false);
        gempack.LoadPanel();
    }
    
    public void GemconfirmationSuccess()
    { // Disable the popup window game object
        gemWindowConfirmation.SetActive(false);
        ShowPopupMessage("Purchase successful");
        gempack.CheckPurchase();
    }
}

