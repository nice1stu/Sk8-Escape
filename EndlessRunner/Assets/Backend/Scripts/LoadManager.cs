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
        public TextMeshProUGUI text;
        private void Awake()
        {
            
            startButton.SetActive(false);
            loadingText.SetActive(true);
            text.text += "0";
            FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
            text.text += "1";
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                StartCoroutine(GetStats());
                text.text += "2";
            }
            else LoadData();
            
        }

        private void LoadData()
        {
            var path = Application.persistentDataPath + "/stats.save.json";
            GameData localData = null;
        
            if (File.Exists(path))
            {
                text.text += " -file exists";
                string json = File.ReadAllText(path);
                localData = JsonUtility.FromJson<GameData>(json);
                localTimeStamp = localData.timeStamp;
            }

            if (localTimeStamp == 0 && onlineTimeStamp == 0)
            {
                text.text += " -neither";

                EnablePressToPlay();
                return;
            }
            
            //get the most up to date data stats
            text.text += " -setting data";

            SetData(localTimeStamp > onlineTimeStamp ? localData : onlineData);
        }

        private void SetData(GameData data)
        {
            SaveManager.SaveTotalScore = data.totalScore;
            SaveManager.SaveTotalGems = data.totalGems;
            SaveManager.SaveTotalCoins = data.totalCoins;
            SaveManager.SaveHighScore = data.playerHighScore;
            EnablePressToPlay();
        }

        //upon finish loading
        private void EnablePressToPlay()
        {
            text.text += " -is it ever here?";
            loadingText.SetActive(false);
            startButton.SetActive(true);
        }

        //get online stats
        public IEnumerator GetStats()
        {
            text.text += "3";
            //give time to fetch
            yield return new WaitForSeconds(1);
            text.text += "4";
            string username = String.Empty;
#if UNITY_ANDROID
            username = GooglePlayGames.PlayGamesPlatform.Instance.localUser.userName;
            text.text += " -user name play games = " + username;

#endif
            if(username == String.Empty) username = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
            text.text += " -user name = " + username;

            var userData = FirebaseDatabase.DefaultInstance.RootReference.Child("users").Child(username).GetValueAsync();
            text.text += " -attempting to get values";

            yield return new WaitUntil(predicate: () => userData.IsCompleted);
            text.text += " -is completed";

            DataSnapshot snapshot = userData.Result;
            //if online data exists
            if (snapshot != null && snapshot.Exists)
            {
                text.text += " -snapshot exists";

                onlineData = JsonUtility.FromJson<GameData>(snapshot.GetRawJsonValue());
                onlineTimeStamp = onlineData.timeStamp;
            }

            text.text += " -loading";
            LoadData();
        }
    }
}
