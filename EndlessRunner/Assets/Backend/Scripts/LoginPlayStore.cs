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
            PlayGamesPlatform.DebugLogEnabled = true;
            Debug.Log("activate");
            PlayGamesPlatform.Activate();
            Debug.Log("start");
            PlayGamesPlatform.Instance.Authenticate(ProcessAutomaticAuth);
        }

        private static void ProcessAutomaticAuth(SignInStatus status)
        {
            Debug.Log("authenticate");
            if (status != SignInStatus.Success)
            {
                Debug.Log("not signed in");
                PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessManualAuth);
            }
            else
            {
                ProcessManualAuth(status);
            }
        }

        private static void ProcessManualAuth(SignInStatus status)
        {
            Debug.Log("authenticate second");
            if (status != SignInStatus.Success)
            {
                Debug.Log("not signed in second");
                return;
            }

            Debug.Log("sign in success");
            PlayGamesPlatform.Instance.RequestServerSideAccess(true, code =>
            {
                Debug.Log("requested server access");
                FirebaseAuth auth = FirebaseAuth.DefaultInstance;
                Credential credential = PlayGamesAuthProvider.GetCredential(code);
                auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
                {
                    Debug.Log("what happens next");
                    if (task.IsCanceled)
                    {
                        Debug.Log("cancelled");
                    }
                    else if (task.IsFaulted)
                    {
                        // error  task.Exception
                        Debug.Log("error" + task.Exception);
                    }
                    else
                    {
                        FirebaseUser newUser = task.Result;
                        Debug.Log("done");
                    }
                });
            });
        }
    }
}