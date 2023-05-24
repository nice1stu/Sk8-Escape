using System;
using System.Collections.Generic;
using Lean.Localization;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor
{
    public class TMPLocalizationStateMenu : EditorWindow 
    {
        private readonly List<TMP_Text> _tmpObjects = new();
        private Vector2 _scrollPos;

        [MenuItem ("Window/TMP Localization State")]
        public static void ShowWindow()
        {
            var window = GetWindow<TMPLocalizationStateMenu>();
            window.titleContent = new GUIContent("TMP Localization State Menu");
            window.Show();
        }


        private void OnGUI () {
            FetchTMPObjects();

            EditorGUILayout.BeginVertical();
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);

            foreach (var tmpObj in _tmpObjects)
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
                if (GUILayout.Button("Focus", GUILayout.MaxWidth(70))) {
                    Selection.activeGameObject = tmpObj.gameObject;
                    SceneView.lastActiveSceneView.FrameSelected();
                }

                EditorGUI.BeginDisabledGroup(hasTranslationComponent);
            
                if (GUILayout.Button("Add", GUILayout.MaxWidth(50))) {
                    if (!hasTranslationComponent) {
                        Undo.AddComponent<LeanLocalizedTextMeshProUGUI>(tmpObj.gameObject).enabled = true;
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
            _tmpObjects.Clear();
        
            foreach (GameObject obj in SceneManager.GetActiveScene().GetRootGameObjects()) {
                TMP_Text[] tmps = obj.GetComponentsInChildren<TMP_Text>(true);

                foreach (TMP_Text tmp in tmps) {
                    _tmpObjects.Add(tmp);
                }
            }
        }

        private static string GetGameObjectPath(GameObject obj) {
            return obj.transform.parent == null ? obj.name : GetGameObjectPath(obj.transform.parent.gameObject) + "/" + obj.name; 
        }

        // returns path "" if not containing /
        private static (string path, string name) RSplitPathName(string split)
        {
            int index = split.LastIndexOf("/", StringComparison.Ordinal);
            return index == -1 ? 
                ("", split) : // not found
                (split[(index + 1)..], split[..index]);
        }
    }
}