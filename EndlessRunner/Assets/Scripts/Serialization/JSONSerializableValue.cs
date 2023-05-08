using System;
using System.IO;
using UnityEngine;

namespace Serialization
{
    public class JSONSerializableValue : MonoBehaviour
    {
        [SerializeField] private SerializableValue coins;
        
        [ContextMenu("Save")]
        private void Save(string pathName)
        {
            SerializableValue data = coins;
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.persistentDataPath + $"/{pathName}.save.json", json);
        }
        [ContextMenu("Load")]
        private void Load(string pathName)
        {
            string path = Application.persistentDataPath + $"/{pathName}.save.json";
            if (!File.Exists(path)) return;

            string json = File.ReadAllText(path);
            SerializableValue data = JsonUtility.FromJson<SerializableValue>(json);
            coins = data;
        }
    }
}
