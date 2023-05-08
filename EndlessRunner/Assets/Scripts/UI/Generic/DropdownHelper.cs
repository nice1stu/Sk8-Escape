using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TMP_Dropdown))]
    public class DropdownHelper : MonoBehaviour
    {
        // cache dropdown reference
        protected TMP_Dropdown DropDown;

        protected string DropDownSelectionText => DropDown.options[DropDown.value].text;
        
        [ExecuteAlways]
        public virtual void Awake()
        {
            DropDown = GetComponent<TMP_Dropdown>();
        }

        protected void PopulateDropdown(IEnumerable<string> arr)
        {
            DropDown.ClearOptions();
            DropDown.AddOptions(arr.ToList());
        }
    }
}