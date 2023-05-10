using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace UI
{
    [RequireComponent(typeof(TMP_Dropdown))]
    public class DropdownHelper : MonoBehaviour
    {
        // cache dropdown reference
        protected TMP_Dropdown DropDown;

        [SerializeField] public UnityEvent postDropdownPopulated = new();

        protected string CurrentText => DropDown.options[DropDown.value].text;
        protected void SetSelection(string text) => DropDown.value = DropDown.options.FindIndex(option => option.text == text);
        
        [ExecuteAlways]
        public virtual void Awake()
        {
            DropDown = GetComponent<TMP_Dropdown>();
        }

        protected void PopulateDropdown(IEnumerable<string> arr)
        {
            DropDown.ClearOptions();
            DropDown.AddOptions(arr.ToList());
            
            postDropdownPopulated.Invoke();
        }
    }
}