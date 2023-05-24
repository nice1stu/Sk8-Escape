using System;
using System.Collections.Generic;
using System.Reflection;
using Lean.Localization;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class LeanLocalizationPrefabHelper : EditorWindow
    {
        private const string LeanLocalizationPrefabPath = "Assets/Localization/Prefabs/LeanLocalization.prefab";
        private const string ScenePrefabName = "LeanLocalization";

        [MenuItem("Window/LeanLocalization Prefab Helper")]
        public static void ShowWindow()
        {
            var window = GetWindow<LeanLocalizationPrefabHelper>();
            window.titleContent = new GUIContent("LeanLocalization Prefab Helper");
            window.Show();
        }

        private GameObject CreateLeanLocalizationWithUndo()
        {
            GameObject prefabAsset = AssetDatabase.LoadAssetAtPath<GameObject>(LeanLocalizationPrefabPath);
            if (prefabAsset == null)
            {
                Debug.LogError($"[{typeof(LeanLocalizationPrefabHelper)}] Prefab not found at: {LeanLocalizationPrefabPath}");
                return null;
            }

            GameObject instansiatedPrefab = PrefabUtility.InstantiatePrefab(prefabAsset) as GameObject;
            Undo.RegisterCreatedObjectUndo(instansiatedPrefab, "Create LeanLocalization Prefab");

            
            return instansiatedPrefab;
        }

        private string _addPhraseEnglishText;
        private string _addPhraseSwedishText;
        private string _addPhraseName;

        // struct TranslatedStringPair
        // {
        //     [NonSerialized] public string English;
        //     [NonSerialized] public string Swedish;
        //
        //     public TranslatedStringPair(string english, string swedish)
        //     {
        //         English = english;
        //         Swedish = swedish;
        //     }
        // }

        // Dictionary<LeanPhrase, TranslatedStringPair> GetPrefabPhrases(List<LeanPhrase> leanPhrases)
        // {
        //     Dictionary<LeanPhrase, TranslatedStringPair> newDict = new(leanPhrases.Count); 
        //     
        //     foreach (var phrase in leanPhrases)
        //     {
        //         newDict[phrase] = new TranslatedStringPair(phrase.Entries[0].Text, phrase.Entries[1].Text);
        //     }
        //
        //     return newDict;
        // }

        private TField GetPrivateField<TField, TInst>(TInst instance, string fieldName)
        {
            Type type = typeof(TInst);
            FieldInfo field = type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);

            return (TField)field.GetValue(instance);
        }
        
        private void SetPrivateField<TField, TInst>(TInst instance, string fieldName, TField setValue )
        {
            Type type = typeof(TInst);
            FieldInfo field = type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);

            field.SetValue(instance, setValue);
        }

        
        private void OnGUI()
        {
            // Check if LeanLocalization prefab exists in scene
            GameObject scenePrefab = GameObject.Find(ScenePrefabName);
            LeanLocalization scenePrefabLocalization;
            bool sceneHasPrefab = scenePrefab != null;
            
            if (!sceneHasPrefab)
            {
                EditorGUILayout.LabelField("No localization prefab.");

                if (GUILayout.Button("Add Prefab"))
                {
                    scenePrefab = CreateLeanLocalizationWithUndo();
                    scenePrefab.transform.SetAsLastSibling(); // Move to bottom in hierarchy

                    Selection.activeGameObject = scenePrefab;
                }
            }
            else
            {
                // ----------- Add Phrase
                EditorGUILayout.LabelField("Add Phrase");
                
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Name");
                EditorGUILayout.LabelField("English");
                EditorGUILayout.LabelField("Swedish");
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.BeginHorizontal();
                _addPhraseName = EditorGUILayout.TextField("", _addPhraseName);
                _addPhraseEnglishText = EditorGUILayout.TextField("", _addPhraseEnglishText);
                _addPhraseSwedishText = EditorGUILayout.TextField("", _addPhraseSwedishText);
                EditorGUILayout.EndHorizontal();
                if (GUILayout.Button("Add Phrase"))
                {
                    GameObject newPhraseGameObject = new GameObject();
                    LeanPhrase newPhrase = newPhraseGameObject.AddComponent<LeanPhrase>(); 

                    List<LeanPhrase.Entry> newEntryList = new(2)
                    {
                        new LeanPhrase.Entry()
                        {
                            Language = "English", Text = _addPhraseEnglishText
                        },
                        new LeanPhrase.Entry()
                        {
                            Language = "Swedish", Text = _addPhraseSwedishText
                        }
                    };
                    
                    SetPrivateField(newPhrase, "entries", newEntryList);

                    newPhraseGameObject.transform.SetParent(scenePrefab.transform);
                }
                // ----------- <\Add Phrase>
                
                EditorGUILayout.LabelField("LeanLocalization Children");
                EditorGUILayout.BeginScrollView(Vector2.zero);
                
                List<LeanLanguage> leanLanguages = new List<LeanLanguage>(2);
                List<LeanPhrase> leanPhrases = new List<LeanPhrase>(20);

                // Gather components
                foreach (Transform child in scenePrefab.transform)
                {
                    var leanLangComponent = child.GetComponent<LeanLanguage>();

                    if (leanLangComponent)
                        leanLanguages.Add(leanLangComponent);
                    else
                    {
                        var leanPhraseComponent = child.GetComponent<LeanPhrase>();
                        if (leanPhraseComponent)
                            leanPhrases.Add(leanPhraseComponent);
                    }
                }
                
                // Display them by category
                
                // Category: Languages 
                EditorGUILayout.LabelField("Languages");
                foreach (var leanLang in leanLanguages)
                {
                    EditorGUILayout.ObjectField(leanLang.gameObject, typeof(LeanLanguage), true);
                }
                EditorGUILayout.Separator(); //
                
                // Category: Phrases
                EditorGUILayout.LabelField("Phrases");

                foreach (var leanPhrase in leanPhrases)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.ObjectField(leanPhrase, typeof(GameObject), true);
                    if (GUILayout.Button("Delete"))
                    {
                       Undo.DestroyObjectImmediate(leanPhrase.gameObject);
                    }
                    EditorGUILayout.EndHorizontal();
                    
                    EditorGUILayout.BeginHorizontal();
                    List<LeanPhrase.Entry> entriesList = GetPrivateField<List<LeanPhrase.Entry>, LeanPhrase>(leanPhrase, "entries");

                    foreach (var entry in entriesList)
                    {
                        EditorGUI.BeginChangeCheck();
                        var changedText = EditorGUILayout.TextField(entry.Text);
                        if (EditorGUI.EndChangeCheck())
                        {
                            entry.Text = changedText;
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.Separator();
                           
                }
            }
            EditorGUILayout.EndScrollView();
        }
    }
}
