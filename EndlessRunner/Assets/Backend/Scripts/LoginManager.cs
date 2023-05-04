using UnityEngine;

namespace Backend.Scripts
{
    public class LoginManager : MonoBehaviour
    {
        public static LoginManager Instance;

        [SerializeField]
        private GameObject loginPanel;

        [SerializeField]
        private GameObject registrationPanel;

        private void Awake()
        {
            CreateInstance();
        }

        private void CreateInstance()
        {
            if(Instance == null)
            {
                Instance = this;
            }
        }

        public void OpenLoginPanel()
        {
            loginPanel.SetActive(true);
            registrationPanel.SetActive(false);
        }

        public void OpenRegistrationPanel()
        {
            registrationPanel.SetActive(true);
            loginPanel.SetActive(false);
        }
    }
}