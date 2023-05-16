using Firebase.Auth;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

namespace Backend.Scripts
{
    public class LoginPlayStore : MonoBehaviour
    {
        private PlayGamesClientConfiguration _clientConfiguration;
        void Start()
        {
            Debug.Log("configure");
            ConfigureGPGS();
            
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
            
            Debug.Log("signin");
            SignInGPGS(SignInInteractivity.CanPromptOnce, _clientConfiguration);
        }

        void ConfigureGPGS()
        {
            Debug.Log("building");
            _clientConfiguration = new PlayGamesClientConfiguration.Builder().Build();
        }

        void SignInGPGS(SignInInteractivity interactivity, PlayGamesClientConfiguration configuration)
        {
            
            configuration = _clientConfiguration;
            Debug.Log("initialize");
            PlayGamesPlatform.InitializeInstance(configuration);
            Debug.Log("activate");
            PlayGamesPlatform.Activate();
            
            PlayGamesPlatform.Instance.Authenticate(interactivity, (code) =>
            {
                Debug.Log("auth");
                if (code == SignInStatus.Success)
                {
                    Debug.Log("signed in");
                }
                else
                {
                    Debug.Log("code is: " + code);
                }
            });
            
            PlayGamesPlatform.Instance.Authenticate(code =>
            {
                Debug.Log("auth");
                if (code)
                {
                    Debug.Log("signed in");
                }
                else
                {
                    Debug.Log("failed");
                }
            });
        }
        
    }
}