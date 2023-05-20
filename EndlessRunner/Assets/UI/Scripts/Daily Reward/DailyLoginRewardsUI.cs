
using UnityEngine;
using UnityEngine.UI;


public class DailyLoginRewardsUI : MonoBehaviour
{
    public GameObject dailyRewardsWindow; //This is the Popup window
    public GameObject rewardPopup;
    public GameObject[] rewardsIcon;
    public DailyRewardsSo[] dailyRewardsSo; //scriptable object for daily rewards
    public GameObject[] dailyRewardsPanelGo; //daily rewards prefabs
    public DailyRewardsTemplate[] dailyRewardsPanel; //template for text
    public GameObject[] claimedGo; //shadowed gameobject when the rewards claimed
    public Button[] claimBtns; //buttons for claiming
    
    private int _currentStreak = 0;
    private const int maxDay = 7;
    
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
        CheckClaimText();
        EnableButtons();
    }

    public void ItemButton(int btnNo)
    {
        if (btnNo == _currentStreak + 1)
        {
            Debug.Log("curent streak: inside loop" + _currentStreak);
        }
        DailyReward();
        dailyRewardsSo[btnNo].claimed = true;
        claimedGo[btnNo].SetActive(true);
        CheckClaimText();
        Debug.Log("curent streak:" + _currentStreak);
        dailyRewardsPanel[btnNo].claimText.text = "Claimed";

        _currentStreak++;
    }
    
    public void CheckClaimText()
    {
        for (int i = 0; i < dailyRewardsSo.Length; i++)
        {
            if (dailyRewardsSo[i].claimed)
            {
                dailyRewardsPanel[i].claimText.text = "Claimed";
                claimedGo[i].SetActive(true);
            }
            else
            {
                dailyRewardsPanel[i].claimText.text = "Unclaim";
                claimedGo[i].SetActive(false);
            }
        }
    }
    

    public void DailyReward()
    {
        ShowPopupReward();
        //TO DO: daily rewards here
    }

    public void ResetStreak()
    {
        
        for (int i = 0; i < dailyRewardsSo.Length; i++)
        {
            claimBtns[i].interactable = false;
            dailyRewardsSo[i].claimed = false;
            claimedGo[i].SetActive(false);
            dailyRewardsPanel[i].claimText.text = "Unclaim";
        }
        _currentStreak = 0;
    }

    public void EnableButtons()
    {
        for (int i = 0; i <= _currentStreak && i < claimBtns.Length; i++)
        {
            claimBtns[_currentStreak].interactable = true;
            dailyRewardsPanel[_currentStreak].claimText.text = "Claim";
        }
    }
    
    public void HideDailyRewardsWindow()
    {
        // Disable the popup window game object
        dailyRewardsWindow.SetActive(false);
    }

    public void ShowPopupReward()
    {
        for (int i = 0; i < rewardsIcon.Length; i++)
        {
            Debug.Log("Im here");
            rewardsIcon[_currentStreak].SetActive(true);
        }
        rewardPopup.SetActive(true);
    }
    public void HidePopupReward()
    {  for (int i = 0; i < rewardsIcon.Length; i++)
        {
            rewardsIcon[i].SetActive(false);
        }
        rewardPopup.SetActive(false);
    }
}
