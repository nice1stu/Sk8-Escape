using System;
using System.Collections;
using System.IO;
using Firebase.Auth;
using Firebase.Database;
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
        private void Awake()
        {
            startButton.SetActive(false);
            loadingText.SetActive(true);
            FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
        
            if(Application.internetReachability != NetworkReachability.NotReachable) StartCoroutine(GetStats());
            else LoadData();
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

            if (localTimeStamp == 0 && onlineTimeStamp == 0)
            {
                EnablePressToPlay();
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
            EnablePressToPlay();
        }

        //upon finish loading
        private void EnablePressToPlay()
        {
            loadingText.SetActive(false);
            startButton.SetActive(true);
        }

        //get online stats
        public IEnumerator GetStats()
        {
            //give time to fetch
            yield return new WaitForSeconds(1);
            string username;
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

            LoadData();
        }
    }
}
