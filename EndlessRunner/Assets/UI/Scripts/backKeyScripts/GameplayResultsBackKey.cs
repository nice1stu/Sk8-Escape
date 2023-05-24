using UnityEngine;
using UnityEngine.UI;

namespace UI.Scripts.backKeyScripts
{
    public class GameplayResultsBackKey : MonoBehaviour
    {
        public Button backButton;
    
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                backButton.onClick.Invoke();
            }
        }
    }
}
