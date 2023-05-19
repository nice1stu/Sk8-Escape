using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DailyLoginRewardsUI : MonoBehaviour
{
    public GameObject dailyRewardsWindow;
    public DailyRewardsSo[] dailyRewardsSo;
    public GameObject[] dailyRewardsPanelGo;
    public DailyRewardsTemplate[] dailyRewardsPanel;
    public GameObject[] claimedGo;
    public Button[] claimBtns;
    
    private int streakDays;
    private int day = 0;
    private int maxDay = 7;
    
    void Start()
    {
        for (int i = 0; i < dailyRewardsSo.Length; i++) //looping through number of SO inside the panel
            dailyRewardsPanelGo[i].SetActive(true);
        LoadPanel();
    }
    
    //this function will update the gameobjects in the scene (text, buttons, etc)
    public void LoadPanel()
    {
        for (int i = 0; i < dailyRewardsSo.Length; i++)
        {
            dailyRewardsPanel[i].rewardTitleTxt.text = dailyRewardsSo[i].title;
            dailyRewardsPanel[i].rewardTxt.text = "" + dailyRewardsSo[i].reward;
        }
        CheckRewardIsClaimed();
    }

    public void ItemButton(int btnNo)
    {
        dailyRewardsSo[btnNo].claimed = true;
        claimedGo[btnNo].SetActive(true);
        DailyReward();
        CheckRewardIsClaimed();
    }
    
    public void CheckRewardIsClaimed()
    {
        // for (int i = day; i < claimBtns.Length; i++)
        // {
        //     
        //     if (day < claimBtns.Length)
        //     {
        //         claimBtns[i].interactable = false;
        //     }
        // }
        
        for (int i = 0 ; i < dailyRewardsSo.Length; i++)
        {
            if (!dailyRewardsSo[i].claimed)
            {
                dailyRewardsPanel[i].claimText.text = "Unclaim";
            }
            else
            {
                dailyRewardsPanel[i].claimText.text = "Claimed";
                claimedGo[i].SetActive(true);
                claimBtns[i].interactable = false;
            }
        }
    }
    
    public void HideDailyRewardsWindow()
    {
        // Disable the popup window game object
        dailyRewardsWindow.SetActive(false);
    }

    public void DailyReward()
    {
        //TO DO: daily rewards here
    }

    public void ResetStreak()
    {
        for (int i = 0; i < dailyRewardsSo.Length; i++)
        {
            claimBtns[i].interactable = true;
            dailyRewardsSo[i].claimed = false;
            claimedGo[i].SetActive(false);
            dailyRewardsPanel[i].claimText.text = "Unclaim";
        }
    }
}
