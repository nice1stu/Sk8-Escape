using System.Collections;
using Firebase.Auth;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;
using UnityEngine;

namespace Backend.Scripts
{
    public class LoginPlayStore : MonoBehaviour
    {
        [Space] [Header("Message")] public TextMeshProUGUI message;

        void Start()
        {
            Debug.Log("start");
            PlayGamesPlatform.Instance.Authenticate(status => {
                Debug.Log("authenticate");
                if (status != SignInStatus.Success)
                {
                    Debug.Log("not signed in");
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
            });
        }
    }
}