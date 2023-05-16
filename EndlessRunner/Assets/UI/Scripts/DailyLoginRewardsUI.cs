using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyLoginRewardsUI : MonoBehaviour
{
    
    public GameObject dailyRewardsWindow;
    public DailyRewardsSO[] dailyRewardsSo;
    public GameObject[] dailyRewardsPanelGo;
    public ShopTemplate[] dailyRewardsPanel;
    public GameObject[] claimedGo;
    public Button[] claimBtns;
    public int days;

    
    void Start()
    {
        for (int i = 0; i < dailyRewardsSo.Length; i++) //looping through number of SO inside the panel
            dailyRewardsPanelGo[i].SetActive(true);
        LoadPanel();
    }
    
    public void LoadPanel()
    {
        for (int i = 0; i < dailyRewardsSo.Length; i++)
        {
            dailyRewardsPanel[i].rewardTitleTxt.text = dailyRewardsSo[i].title;
            dailyRewardsPanel[i].rewardTxt.text = "Coins: " + dailyRewardsSo[i].rewardCoins;
            dailyRewardsPanel[i].rewardTxt.text = "Coins: " + dailyRewardsSo[i].rewardGems;
        }
    }

    public void CheckRewardIsClaimed()
    {
        for (int i = 0; i < claimedGo.Length; i++)
        {
            
        }
    }
    
    public void HideDailyRewardsWindow()
    {
        // Disable the popup window game object
        dailyRewardsWindow.SetActive(false);
    }
    
    
}
