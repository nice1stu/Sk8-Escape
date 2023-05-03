using UnityEngine;

namespace Backend.Scripts
{
    public class LoginManager : MonoBehaviour
    {
        private static LoginManager _instance;

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
            if(_instance == null)
            {
                _instance = this;
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