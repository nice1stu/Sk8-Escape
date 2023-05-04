using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Ads.Scripts
{
    public class AdInterstitialDisplay : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
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
        
        private void Awake()
        {
            InitializeAds();
        }
        private void Update()
        {
            //Run the ad
            if (Advertisement.isInitialized && !adStarted && GameplayResults.hasPlayedAd)
            {
                LoadAd(); 
            }
        }

        public void InitializeAds()
        {
            myAdUnitId = adUnitIdAndroid;
            if (!Advertisement.isInitialized && Advertisement.isSupported)
            {
                Advertisement.Initialize(myGameIdAndroid, testMode, this);
            }
        }
        
        // Load content to the Ad Unit:
        public void LoadAd()
        {
            // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
            Debug.Log("Loading Ad: " + myAdUnitId);
            Advertisement.Load(myAdUnitId, this);
        }
        
        // Show the loaded content in the Ad Unit:
        public void ShowAd()
        {
            // Note that if the ad content wasn't previously loaded, this method will fail
            Debug.Log("Showing Ad: " + myAdUnitId);
            Advertisement.Show(myAdUnitId, this);
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Unity Ads initialization complete.");

        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");        
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
            ShowAd();
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Error loading Ad Unit: {myAdUnitId} - {error.ToString()} - {message}");
            // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Error showing Ad Unit {myAdUnitId}: {error.ToString()} - {message}");
            // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            adStarted = true;
        }

        public void OnUnityAdsShowClick(string placementId)
        {
            throw new NotImplementedException();
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            //throw new NotImplementedException();
        }
    }
}
