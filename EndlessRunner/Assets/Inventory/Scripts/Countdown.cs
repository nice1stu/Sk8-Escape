using System;
using System.Globalization;
using UnityEngine;

namespace Inventory.Scripts
{
    public class Countdown : MonoBehaviour
    {
        // Use this to set the timer text
        public TimeSpan timeLeft;
        
        // Time when loot box is added to the loot box inventory
        private DateTime _startTime;
        // The time the loot box takes to open
        private TimeSpan _lootBoxTime;
        // The loot box
        private BaseLootBox _lootBox;

        // This function is called when the loot box is added to the loot box inventory
        // it sets start time to the current time in UTC timezone, so it is time zone independent
        public void StartCountdown(BaseLootBox lootBox)
        {
            _startTime = DateTime.UtcNow;
            _lootBoxTime = lootBox.TimeToOpen;
            _lootBox = lootBox;
        }
        
        private void Update()
        {
            // only checks if any loot box has been added (is in the loot box inventory)
            if (_lootBox != null)
            {
                // takes current time in UTC time zone and subtracts start time
                var timeFromStart = DateTime.UtcNow - _startTime;
                // if the time the loot box takes to open has passed
                if (timeFromStart >= _lootBoxTime)
                {
                    _lootBox.OpenLootBox();
                    // sets loot box to null so the countdown knows it isn't counting down anymore
                    _lootBox = null;
                    return;
                }
                // updates time left for the text
                timeLeft = _lootBoxTime - timeFromStart;
            }
        }
        
        // this saves the start time so when the game is paused the countdown will still work
        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                PlayerPrefs.SetString("startTime", _startTime.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                if (PlayerPrefs.HasKey("startTime"))
                {
                    _startTime = DateTime.Parse(PlayerPrefs.GetString("startTime"));
                }
                else
                {
                    _startTime = DateTime.Now;
                }
            }
        }
    }
}
