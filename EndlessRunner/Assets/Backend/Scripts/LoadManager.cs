using System;
using System.Collections;
using System.IO;
using Firebase.Auth;
using Firebase.Database;
using TMPro;
using UnityEngine;

namespace Backend.Scripts
{
    public class LoadManager : MonoBehaviour
    {
        private long onlineTimeStamp;
        private long localTimeStamp;
        private GameData onlineData;
        private SaveManager _saveManager;

        public GameObject startButton;
        public GameObject loadingText;
        public TextMeshProUGUI signinFailed;
        
        private bool OnlineDataMissing => onlineTimeStamp == 0;
        private bool LocalDataMissing => localTimeStamp == 0;
        private void Awake()
        {
            EnablePressToPlay(false);
            Invoke("HandleFailedSignin", 5);
            FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                StartCoroutine(GetStats());
            }
            else
            {
                CancelInvoke("HandleFailedSignin");
                HandleFailedSignin();
            }
        }

        private void LoadData()
        {
            var path = Application.persistentDataPath + "/stats.save.json";
            GameData localData = null;
        
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                localData = JsonUtility.FromJson<GameData>(json);
                localTimeStamp = localData.timeStamp;
            }

            if (LocalDataMissing && OnlineDataMissing)
            {
                EnablePressToPlay(true);
                return;
            }
            //get the most up to date data stats
            SetData(localTimeStamp > onlineTimeStamp ? localData : onlineData);
        }

        private void SetData(GameData data)
        {
            SaveManager.SaveTotalScore = data.totalScore;
            SaveManager.SaveTotalGems = data.totalGems;
            SaveManager.SaveTotalCoins = data.totalCoins;
            SaveManager.SaveHighScore = data.playerHighScore;
            EnablePressToPlay(true);
        }

        //upon finish loading
        private void EnablePressToPlay(bool b)
        {
            loadingText.SetActive(!b);
            startButton.SetActive(b);
        }

        //get online stats
        public IEnumerator GetStats()
        {
            //give time to fetch
            yield return new WaitForSeconds(1);
            string username = String.Empty;
#if UNITY_ANDROID
            username = GooglePlayGames.PlayGamesPlatform.Instance.localUser.userName;
#endif
            if(username == String.Empty) username = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
            var userData = FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(username).GetValueAsync();
            
            yield return new WaitUntil(predicate: () => userData.IsCompleted);

            DataSnapshot snapshot = userData.Result;
            //if online data exists
            if (snapshot != null && snapshot.Exists)
            {
                onlineData = JsonUtility.FromJson<GameData>(snapshot.GetRawJsonValue());
                onlineTimeStamp = onlineData.timeStamp;
            }
            CancelInvoke("HandleFailedSignin");
            LoadData();
        }

        void HandleFailedSignin()
        {
            signinFailed.text += "Failed to fetch online data.";
            LoadData();
            EnablePressToPlay(true);
        }
    }
}
