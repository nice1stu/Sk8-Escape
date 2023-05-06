using System;
using UnityEngine;

namespace Serialization
{
    [Serializable]
    public struct SerializableValue : ISerializableValue
    {
        [SerializeField] private string key;
        [SerializeField] private int value;

        public string Key
        {
            readonly get => key;
            set => key = value;
        }

        public int Value
        {
            readonly get => value;
            set
            {
                this.value = value;
                ValueChanged?.Invoke(value);
            } 
        }
    

        public event Action<int> ValueChanged;
    }
}
