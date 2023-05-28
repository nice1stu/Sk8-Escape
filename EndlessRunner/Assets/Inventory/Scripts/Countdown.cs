using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.Scripts
{
    public class Countdown : MonoBehaviour
    {

        // The loot box
        private ILootBoxData _lootBox;

        public TextMeshProUGUI countDownLabel;
        public Image checkMark;

        // This function is called when the loot box is added to the loot box inventory
        // it sets start time to the current time in UTC timezone, so it is time zone independent
        public void StartCountdown(ILootBoxData lootBox)
        {
            _lootBox = lootBox;
        }

        public void StopCountDown()
        {
            _lootBox = null;
            countDownLabel.text = String.Empty;
        }
        
        private void Update()
        {
            // only checks if any loot box has been added (is in the loot box inventory)
            if (_lootBox != null)
            {
                // takes current time in UTC time zone and subtracts start time
                var timeFromStart = DateTime.UtcNow - _lootBox.OpeningStartTime;
                // if the time the loot box takes to open has passed
                if (timeFromStart >= _lootBox.Config.TimeToOpen)
                {
                    //Dependencies.Instance.LootBoxes.OpenLootBox(_lootBox);
                    //Todo: add Glow on box when its ready to be opened
                    StopCountDown();
                    return;
                }
                // updates time left for the text
                countDownLabel.text = (_lootBox.Config.TimeToOpen - timeFromStart).ToString(@"hh\:mm\:ss");
            }
        }
    }
}
