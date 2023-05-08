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
            PlayGamesPlatform.Instance.Authenticate(status =>
            {
                if (status == SignInStatus.Success)
                {
                    PlayGamesPlatform.Instance.RequestServerSideAccess(true, code =>
                    {
                        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
                        Credential credential = PlayGamesAuthProvider.GetCredential(code);
                        // auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
                        // {
                        //     if (task.IsCanceled)
                        //     {
                        //         // canceled
                        //     }
                        //     else if (task.IsFaulted)
                        //     {
                        //         // error  task.Exception
                        //     }
                        //     else
                        //     {
                        //         FirebaseUser newUser = task.Result;
                        //     }
                        // });
                        StartCoroutine(AuthGet());

                        IEnumerator AuthGet()
                        {
                            System.Threading.Tasks.Task<FirebaseUser> task = auth.SignInWithCredentialAsync(credential);
                            while (!task.IsCompleted) yield return null;
                            if (task.IsCanceled)
                            {
                                message.text += "Auth canceled";
                            }
                            else if (task.IsFaulted)
                            {
                                message.text += "Faulted" + task.Exception;
                            }
                            else
                            {
                                FirebaseUser newUser = task.Result;
                                message.text += "Fully authenticated user";
                            }
                        }
                    });
                }

                message.text += "PGP authenticate failed";
            });
        }
    }
}