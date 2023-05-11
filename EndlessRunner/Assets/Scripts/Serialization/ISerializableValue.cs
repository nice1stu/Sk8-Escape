using System;

namespace Serialization
{
    public interface ISerializableValue
    {
        string Key { get; set; }
        int Value { get; set; }

        event Action<int> ValueChanged;
    }
}