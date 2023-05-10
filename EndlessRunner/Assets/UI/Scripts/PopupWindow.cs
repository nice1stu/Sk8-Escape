using UnityEngine;
using TMPro;

public class PopupWindow : MonoBehaviour
{
    public GameObject popupWindow; // Reference to the popup window game object
    public TextMeshProUGUI messageText; // Reference to the text component displaying the message
    
    // Method to display the popup window with the given message
    public void ShowPopup(string message)
    {
        // Enable the popup window game object
        popupWindow.SetActive(true);
        // Set the message text
        messageText.text = message;
    }
    
    // Method to hide the popup window
    public void HidePopup()
    {
        // Disable the popup window game object
        popupWindow.SetActive(false);
    }
}
