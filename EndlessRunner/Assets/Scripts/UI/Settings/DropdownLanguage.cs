using System.Collections.Generic;
using Lean.Localization;
using TMPro;
using UnityEngine;

namespace UI.Settings
{
    public class DropdownLanguage : DropdownHelper
    {
        [ExecuteAlways]
        public override void Awake()
        {
            base.Awake(); // caches dropdown component
        }

        private void Start()
        {
            // Awake causes it to attempt populating before languages are available 
            PopulateDropdown(GetCurrentLanguageList()); 
        }
        
        private static IEnumerable<string> GetCurrentLanguageList()
        {
            Debug.Assert(LeanLocalization.CurrentLanguages.Count != 0, "No languages defined, are you missing a prefab?");

            var langsArr = new string[LeanLocalization.CurrentLanguages.Count];
        
            var index = 0;
            foreach ((string lang, _) in LeanLocalization.CurrentLanguages)
            {
                langsArr[index++] = lang;
            }

            return langsArr;
        }

        [ContextMenu("Populate Dropdown with Scene Languages")]
        private void PopulateWithLanguages()
        {
            PopulateDropdown(GetCurrentLanguageList());
        }

        [ContextMenu("Set On Value Changed to Update Language")]
        private void SetupOnChanged()
        {
            DropDown.onValueChanged.AddListener(delegate
            {
                LeanLocalization.SetCurrentLanguageAll(DropDownSelectionText);
            });
        }
    
    }
}
