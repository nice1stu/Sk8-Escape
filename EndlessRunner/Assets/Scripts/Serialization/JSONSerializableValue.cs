using System;
using System.IO;
using UnityEngine;

namespace Serialization
{
    public class JSONSerializableValue : MonoBehaviour
    {
        [SerializeField] private SerializableValue coins;
        
        [ContextMenu("Save")]
        private void Save()
        {
            SerializableValue data = coins;
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.persistentDataPath + "/inventory.save.json", json);
        }
        [ContextMenu("Load")]
        private void Load()
        {
            string path = Application.persistentDataPath + "/inventory.save.json";
            if (!File.Exists(path)) return;

            string json = File.ReadAllText(path);
            SerializableValue data = JsonUtility.FromJson<SerializableValue>(json);
            coins = data;
        }
    }
}
