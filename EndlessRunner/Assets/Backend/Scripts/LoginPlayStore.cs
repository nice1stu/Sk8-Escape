using System.Threading.Tasks;
using Firebase.Auth;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

namespace Backend.Scripts
{
    public class LoginPlayStore : MonoBehaviour
    {
        void Start()
        {
#if UNITY_ANDROID
            
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
            PlayGamesPlatform.Instance.Authenticate(ProcessAutomaticAuth);
#else 
            FirebaseAuth auth = FirebaseAuth.DefaultInstance;
            auth.SignInAnonymouslyAsync().ContinueWith(ContinueWithLogin);
#endif
        }
#if UNITY_ANDROID

        private static void ProcessAutomaticAuth(SignInStatus status)
        {
            if (status != SignInStatus.Success)
            {
                PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessManualAuth);
            }
            else
            {
                ProcessManualAuth(status);
            }
        }

        private static void ProcessManualAuth(SignInStatus status)
        {
            if (status != SignInStatus.Success) return;

            PlayGamesPlatform.Instance.RequestServerSideAccess(true, code =>
            {
                FirebaseAuth auth = FirebaseAuth.DefaultInstance;
                Credential credential = PlayGamesAuthProvider.GetCredential(code);
                auth.SignInWithCredentialAsync(credential).ContinueWith(ContinueWithLogin);
            });
        }
#endif
        private static void ContinueWithLogin(Task<FirebaseUser> task)
        {
                if (task.IsCanceled) Debug.Log("cancelled");
                else if (task.IsFaulted)
                {
                    Debug.Log("error" + task.Exception);
                }
                else
                {
                    FirebaseUser newUser = task.Result;
                } 
        }
        
    }
}