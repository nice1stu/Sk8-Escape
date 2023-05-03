using UnityEngine;
using UnityEngine.Advertisements;

namespace Ads.Scripts
{
    public class AdInterstitialDisplay : MonoBehaviour
    {
        //The game id to access ads
        public string myGameIdAndroid = "5266579";
        //The type of ad
        public string adUnitIdAndroid = "Interstitial_Android";
        //Ad id that is being used
        public string myAdUnitId;
        //Keep track if ad has started
        public bool adStarted;
        //If you want to be in test mode or not
        public bool testMode = true;

        private void Start()
        {
            //Initialize the ad
            Advertisement.Initialize(myGameIdAndroid, testMode);
            myAdUnitId = adUnitIdAndroid;
        }

        private void Update()
        {
            //Run the ad
            if (Advertisement.isInitialized && !adStarted)
            {
                Advertisement.Load(myAdUnitId);
                Advertisement.Show(myAdUnitId);
                adStarted = true;
            }
        }
    }
}
