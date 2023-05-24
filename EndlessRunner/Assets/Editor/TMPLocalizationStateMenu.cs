using System;
using System.Collections.Generic;
using System.Linq;
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


        public static Dictionary<string, LeanPhrase> GetPhraseNamePairs()
        {
            return (
                from Transform child in LeanLocalization.GetOrCreateInstance().transform 
                select child.GetComponent<LeanPhrase>() 
                into childLeanPhrase 
                where childLeanPhrase select childLeanPhrase
                ).ToDictionary(childLeanPhrase => childLeanPhrase.gameObject.name, childLeanPhrase => childLeanPhrase);
        }
        
        private void OnGUI () {
            FetchTMPObjects();

            EditorGUILayout.BeginVertical();
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);

            foreach (var tmpObj in _tmpObjects)
            {
                EditorGUILayout.BeginHorizontal();

            
                var translationComponent = tmpObj.GetComponent<LeanLocalizedTextMeshProUGUI>();
                bool hasTranslationComponent = translationComponent;
            
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.Toggle(hasTranslationComponent, GUILayout.Width(15)); // toggle readonly anyways
                EditorGUI.EndDisabledGroup();
            
                // EditorGUILayout.LabelField(GetGameObjectPath(tmpObjects[i].gameObject), GUILayout.MaxWidth(400));

                (string goPath, string goName) = RSplitPathName( GetGameObjectPath(tmpObj.gameObject) );
            
                EditorGUILayout.LabelField(goPath, GUILayout.MaxWidth(300));
                EditorGUILayout.LabelField(goName);
            
                // ------------------------------- Buttons
                // Select
                if (GUILayout.Button("Select", GUILayout.MaxWidth(60))) {
                    Selection.activeGameObject = tmpObj.gameObject;
                }
                
                // Focus
                if (GUILayout.Button("Focus", GUILayout.MaxWidth(60))) {
                    Selection.activeGameObject = tmpObj.gameObject;
                    SceneView.lastActiveSceneView.FrameSelected();
                }

                EditorGUI.BeginDisabledGroup(hasTranslationComponent);
            
                // Add
                if (GUILayout.Button("Add", GUILayout.MaxWidth(40))) {
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
                
                EditorGUI.BeginDisabledGroup(!hasTranslationComponent);

                Dictionary<string, LeanPhrase> phraseNamePairs = GetPhraseNamePairs();
                
                string[] phraseNames = phraseNamePairs.Keys.ToArray();
                
                if (EditorGUILayout.DropdownButton(new GUIContent("Set"), FocusType.Passive, GUILayout.MaxWidth(40)))
                {
                    GenericMenu menu = new GenericMenu();
                    foreach (var phraseName in phraseNames)
                    {
                        bool isCurrentTranslation = translationComponent.TranslationName == phraseName;
                        menu.AddItem(new GUIContent(phraseName), isCurrentTranslation, () =>
                        {
                            translationComponent.TranslationName = phraseName;
                        });
                        menu.ShowAsContext();
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