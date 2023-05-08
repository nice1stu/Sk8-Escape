using System;
using UnityEngine;

namespace Stat
{
    [Serializable]
    public struct Stats : IStats
    {
        [SerializeField] private int speed;
        [SerializeField] private int stability;
        [SerializeField] private int style;
        [SerializeField] private int balance;

        public int Speed
        {
            readonly get => speed;
            set => speed = value;
        }

        public int Stability
        {
            readonly get => stability;
            set => stability = value;
        }

        public int Style
        {
            readonly get => style;
            set => style = value;
        }

        public int Balance
        {
            readonly get => balance;
            set => balance = value;
        }
    }
}