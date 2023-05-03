using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Ads.Scripts
{
    public class AdRewardedDisplay : MonoBehaviour
    {
        public string myGameIdAndroid = "5266579";
        public string adUnitIdAndroid = "Rewarded_Android";
        public string myAdUnitId;
        public bool adStarted;
        public bool testMode = true;

        private void Start()
        {
            Advertisement.Initialize(myGameIdAndroid, testMode);
            myAdUnitId = adUnitIdAndroid;
        }

        private void Update()
        {
            if (Advertisement.isInitialized && !adStarted)
            {
                Advertisement.Load(myAdUnitId);
                Advertisement.Show(myAdUnitId);
                adStarted = true;
            }
        }
    }
}
