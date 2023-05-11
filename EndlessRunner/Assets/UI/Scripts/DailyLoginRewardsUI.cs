using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyLoginRewardsUI : MonoBehaviour
{
    
    public GameObject dailyRewardsWindow;
    
    public void HideDailyRewardsWindow()
    {
        // Disable the popup window game object
        dailyRewardsWindow.SetActive(false);
    }
}
