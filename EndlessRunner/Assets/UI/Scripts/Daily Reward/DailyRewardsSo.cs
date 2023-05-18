using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/ Daily Rewards", order = 3)]
public class DailyRewardsSo : ScriptableObject
{
    public string title;
    public int reward;
    public bool claimed;
}
