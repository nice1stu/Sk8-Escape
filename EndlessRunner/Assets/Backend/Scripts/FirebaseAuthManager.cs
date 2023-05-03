using System.Collections;
using Firebase;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.UI;

namespace Backend.Scripts
{
    public class FirebaseAuthManager : MonoBehaviour
    {
        public static string UserName = "";
        // Firebase variable
        [Header("Firebase")]
        public DependencyStatus dependencyStatus;
        public FirebaseAuth auth;
        public FirebaseUser user;

        // Login Variables
        [Space]
        [Header("Login")]
        public InputField emailLoginField;
        public InputField passwordLoginField;

        // Registration Variables
        [Space]
        [Header("Registration")]
        public InputField nameRegisterField;
        public InputField emailRegisterField;
        public InputField passwordRegisterField;
        public InputField confirmPasswordRegisterField;

        private void Awake()
        {
            // Check that all of the necessary dependencies for firebase are present on the system
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                dependencyStatus = task.Result;

                if (dependencyStatus == DependencyStatus.Available)
                {
                    InitializeFirebase();
                }
                else
                {
                    Debug.LogError("Could not resolve all firebase dependencies: " + dependencyStatus);
                }
            });
        }

        void InitializeFirebase()
        {
            //Set the default instance object
            auth = FirebaseAuth.DefaultInstance;

            auth.StateChanged += AuthStateChanged;
            AuthStateChanged(this, null);
        }

        // Track state changes of the auth object.
        void AuthStateChanged(object sender, System.EventArgs eventArgs)
        {
            if (auth.CurrentUser == user) return;
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;

            if (!signedIn && user != null) Debug.Log("Signed out " + user.UserId);

            user = auth.CurrentUser;

            if (signedIn) Debug.Log("Signed in " + user.UserId);
        }

        public void Login()
        {
            StartCoroutine(LoginAsync(emailLoginField.text, passwordLoginField.text));
        }

        private IEnumerator LoginAsync(string email, string password)
        {
            var loginTask = auth.SignInWithEmailAndPasswordAsync(email, password);

            yield return new WaitUntil(() => loginTask.IsCompleted);

            if (loginTask.Exception != null)
            {
                Debug.LogError(loginTask.Exception);

                FirebaseException firebaseException = loginTask.Exception.GetBaseException() as FirebaseException;
                AuthError authError = (AuthError)firebaseException.ErrorCode;
                
                string failedMessage = "Login Failed! Because ";

                // For developers, todo: add a fail message for build and display it in game
                switch (authError)
                {
                    case AuthError.InvalidEmail:
                        failedMessage += "Email is invalid";
                        break;
                    case AuthError.WrongPassword:
                        failedMessage += "Wrong Password";
                        break;
                    case AuthError.MissingEmail:
                        failedMessage += "Email is missing";
                        break;
                    case AuthError.MissingPassword:
                        failedMessage += "Password is missing";
                        break;
                    default:
                        failedMessage = "Login Failed";
                        break;
                }

                Debug.Log(failedMessage);
            }
            else
            {
                user = loginTask.Result;

                //todo: Display this in game
                Debug.LogFormat("{0} You Are Successfully Logged In", user.DisplayName);

                UserName = user.DisplayName;
                
                //todo: Ask which scene to be loaded
                // UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
            }
        }

        public void Register()
        {
            StartCoroutine(RegisterAsync(nameRegisterField.text, emailRegisterField.text, passwordRegisterField.text, confirmPasswordRegisterField.text));
        }

        private IEnumerator RegisterAsync(string name, string email, string password, string confirmPassword)
        {
            //todo: display it in game
            if (name == "")
            {
                Debug.LogError("User Name is empty");
            }
            else if (email == "")
            {
                Debug.LogError("email field is empty");
            }
            else if (passwordRegisterField.text != confirmPasswordRegisterField.text)
            {
                Debug.LogError("Password does not match");
            }
            else
            {
                var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);

                yield return new WaitUntil(() => registerTask.IsCompleted);

                if (registerTask.Exception != null)
                {
                    Debug.LogError(registerTask.Exception);

                    FirebaseException firebaseException = registerTask.Exception.GetBaseException() as FirebaseException;
                    AuthError authError = (AuthError)firebaseException.ErrorCode;

                    //todo: display in game
                    string failedMessage = "Registration Failed! Because ";
                    switch (authError)
                    {
                        case AuthError.InvalidEmail:
                            failedMessage += "Email is invalid";
                            break;
                        case AuthError.WrongPassword:
                            failedMessage += "Wrong Password";
                            break;
                        case AuthError.MissingEmail:
                            failedMessage += "Email is missing";
                            break;
                        case AuthError.MissingPassword:
                            failedMessage += "Password is missing";
                            break;
                        default:
                            failedMessage = "Registration Failed";
                            break;
                    }

                    Debug.Log(failedMessage);
                }
                else
                {
                    // Get The User After Registration Success
                    user = registerTask.Result;

                    UserProfile userProfile = new UserProfile { DisplayName = name };

                    var updateProfileTask = user.UpdateUserProfileAsync(userProfile);

                    yield return new WaitUntil(() => updateProfileTask.IsCompleted);

                    if (updateProfileTask.Exception != null)
                    {
                        // Delete the user if user update failed
                        user.DeleteAsync();

                        Debug.LogError(updateProfileTask.Exception);

                        FirebaseException firebaseException = updateProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError authError = (AuthError)firebaseException.ErrorCode;


                        string failedMessage = "Profile update Failed! Becuase ";
                        switch (authError)
                        {
                            case AuthError.InvalidEmail:
                                failedMessage += "Email is invalid";
                                break;
                            case AuthError.WrongPassword:
                                failedMessage += "Wrong Password";
                                break;
                            case AuthError.MissingEmail:
                                failedMessage += "Email is missing";
                                break;
                            case AuthError.MissingPassword:
                                failedMessage += "Password is missing";
                                break;
                            default:
                                failedMessage = "Profile update Failed";
                                break;
                        }

                        Debug.Log(failedMessage);
                    }
                    else
                    {
                        //todo: display this in game
                        Debug.Log("Registration Successful Welcome " + user.DisplayName);
                        LoginManager.Instance.OpenLoginPanel();
                    }
                }
            }
        }
    }
}
