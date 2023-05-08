using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DropdownHelper : MonoBehaviour
{
    protected void PopulateDropdown(IEnumerable<string> arr)
    {
        var targetDropdown = GetComponent<TMP_Dropdown>();
        targetDropdown.ClearOptions();
        targetDropdown.AddOptions(arr.ToList());
    }
}