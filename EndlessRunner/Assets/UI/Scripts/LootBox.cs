using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Scripts
{
    public class LootBox : MonoBehaviour, IInventoryItem
    {
        public int timeRemainingValue;

        public int TimeRemainingValue
        {
            get => timeRemainingValue;
            set
            {
                if (value < 0) timeRemainingValue = 0;
                // 99:59:59 is the max time
                else if (value > 359999) timeRemainingValue = 359999;
                else timeRemainingValue = value;
            }
        }
        
        public TextMeshProUGUI timeRemainingDisplay;
        public Image itemIcon;
        public Image discardButton;

        public void DiscardItem() { throw new System.NotImplementedException(); }
        
        // change return type when Item class (or smth) is implemented
        public string GetReward() { throw new System.NotImplementedException(); }

        // initialize values here
        void Start()
        {
            // timeRemainingValue = 359999;
            timeRemainingDisplay.text = INTToClockString(timeRemainingValue);
        }

        static string INTToClockString(int time)
        {
            int hours = time / 3600;
            time -= hours * 3600;
            int minutes = time / 60;
            time -= minutes * 60;
            int seconds = time;

            return $"{(hours < 10 ? "0" + hours : hours)}:{(minutes < 10 ? "0" + minutes : minutes)}:{seconds}";
        }
    }
}
