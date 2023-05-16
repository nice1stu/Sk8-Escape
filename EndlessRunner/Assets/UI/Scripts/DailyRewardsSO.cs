using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/ Daily Rewards", order = 3)]
public class DailyRewardsSO : ScriptableObject
{
    public string title;
    public int rewardCoins;
    public int rewardGems;
    public bool claimed;
}
