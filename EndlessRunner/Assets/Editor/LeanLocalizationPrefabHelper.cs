using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class LeanLocalizationPrefabHelper : EditorWindow
    {
     
        [MenuItem("Window/LeanLocalization Prefab Helper")]
        public void ShowWindow() => GetWindow(typeof(LeanLocalizationPrefabHelper));

        private void OnGUI()
        {
            // Check if LeanLocalization prefab exists in scene
            GameObject prefab = Resources.Load<GameObject>("Localization/Prefabs/LeanLocalization");
            GameObject existingPrefab = GameObject.Find("LeanLocalization");
            if (existingPrefab == null)
            {
                EditorGUILayout.LabelField("No localization prefab.");
                if (GUILayout.Button("Add prefab"))
                {
                    if (prefab != null)
                    {
                        GameObject newPrefab = Instantiate(prefab);
                        newPrefab.name = "LeanLocalization";
                        Selection.activeGameObject = newPrefab;
                    }
                    else
                    {
                        Debug.LogWarning("Localization prefab not found. Make sure it exists at 'Assets/Localization/Prefabs/LeanLocalization.prefab'.");
                    }
                }
            }
            else
            {
                EditorGUILayout.LabelField("Children of LeanLocalization:");
                EditorGUILayout.BeginScrollView(Vector2.zero);
                foreach (Transform child in existingPrefab.transform)
                {
                    EditorGUILayout.ObjectField(child.gameObject, typeof(GameObject), true);
                }
                EditorGUILayout.EndScrollView();
            }
        }
    }
}