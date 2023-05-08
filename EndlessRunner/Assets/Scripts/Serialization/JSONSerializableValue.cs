using System.IO;
using UnityEngine;

namespace Serialization
{
    public class JSONSerializableValue : MonoBehaviour
    {
        [ContextMenu("Save")]
        private void Save(SerializableValue value, string pathName)
        {
            var json = JsonUtility.ToJson(value);
            File.WriteAllText(Application.persistentDataPath + $"/{pathName}.save.json", json);
        }

        [ContextMenu("Load")]
        private SerializableValue? Load(string pathName)
        {
            var path = Application.persistentDataPath + $"/{pathName}.save.json";
            if (!File.Exists(path)) return null;

            var json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<SerializableValue>(json);
            return data;
        }
    }
}