using UnityEngine;
using UnityEditor;
using TMPro;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Codice.Client.Common;
using Lean.Localization;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class TMPLocalizationStateMenu : EditorWindow {
    private List<TMP_Text> tmpObjects = new();
    private Vector2 scrollPos;
    [MenuItem ("Window/TMP Localization State")]
    public static void  ShowWindow () {
        GetWindow(typeof(TMPLocalizationStateMenu));
    }

    void OnGUI () {
        FetchTMPObjects();

        EditorGUILayout.BeginVertical();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        foreach (var tmpObj in tmpObjects)
        {
            EditorGUILayout.BeginHorizontal();

            
            bool hasTranslationComponent = tmpObj.GetComponent<LeanLocalizedTextMeshProUGUI>();
            
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.Toggle(hasTranslationComponent, GUILayout.Width(15)); // toggle readonly anyways
            EditorGUI.EndDisabledGroup();
            
            // EditorGUILayout.LabelField(GetGameObjectPath(tmpObjects[i].gameObject), GUILayout.MaxWidth(400));

            (string goPath, string goName) = RSplitPathName( GetGameObjectPath(tmpObj.gameObject) );
            
            EditorGUILayout.LabelField(goPath, GUILayout.MaxWidth(300));
            EditorGUILayout.LabelField(goName);
            
            // ------------------------------- Buttons
            if (GUILayout.Button("Select", GUILayout.MaxWidth(70))) {
                Selection.activeGameObject = tmpObj.gameObject;
            }

            EditorGUI.BeginDisabledGroup(hasTranslationComponent);
            
            if (GUILayout.Button("Add", GUILayout.MaxWidth(50))) {
                if (!hasTranslationComponent) {
                    Undo.AddComponent<TextMeshProUGUI>(tmpObj.gameObject).enabled = true;
                    Selection.activeGameObject = tmpObj.gameObject;
                }
                else
                {
                    GUI.enabled = false;
                }
            }

            EditorGUI.EndDisabledGroup();
            // </Buttons> -------------------------------
            
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }

    private void FetchTMPObjects() {
        tmpObjects.Clear();
        
        foreach (GameObject obj in SceneManager.GetActiveScene().GetRootGameObjects()) {
            TMP_Text[] tmps = obj.GetComponentsInChildren<TMP_Text>(true);

            foreach (TMP_Text tmp in tmps) {
                tmpObjects.Add(tmp);
            }
        }
    }

    private string GetGameObjectPath(GameObject obj) {
        return obj.transform.parent == null ? obj.name : GetGameObjectPath(obj.transform.parent.gameObject) + "/" + obj.name; 
    }

    // returns path "" if not containing /
    private (string path, string name) RSplitPathName(string split)
    {
        int index = split.LastIndexOf("/");
        return index == -1 ? 
            ("", split) : // not found
            (split[(index + 1)..], split[..index]);
    }
}