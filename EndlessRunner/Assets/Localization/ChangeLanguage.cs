using Lean.Localization;
using UnityEditor;
using UnityEngine;

namespace Localization
{
    public class ChangeLanguage : MonoBehaviour
    {
        public LeanLocalization localizationPrefab;
        
        public void Change()
        {
            if (LeanLocalization.GetFirstCurrentLanguage() == "English")
                LeanLocalization.SetCurrentLanguageAll("Swedish");
            else
                LeanLocalization.SetCurrentLanguageAll("English");
        }
    }
}
