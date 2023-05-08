using System.Linq;
using TMPro;
using UnityEngine;

public class DropdownHelper : MonoBehaviour
{
    protected void PopulateDropdown(string[] arr)
    {
        var targetDropdown = GetComponent<TMP_Dropdown>();
        targetDropdown.ClearOptions();
        targetDropdown.AddOptions(arr.ToList());
    }
}