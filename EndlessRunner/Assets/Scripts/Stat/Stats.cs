using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Stat
{
    [Serializable]
    public struct Stats : IStats
    {
        [SerializeField] private int coffinTimeAdded;
        [SerializeField] private int stability;
        [SerializeField] private int vision;
        [SerializeField] private int grindMiniGameBallSize;
        [SerializeField] private int grindLeniency;
        [SerializeField] private int scoreMultiplier;

        public int CoffinTimeAdded
        {
            readonly get => coffinTimeAdded;
            set => coffinTimeAdded = value;
        }

        public int Stability
        {
            readonly get => stability;
            set => stability = value;
        }

        public int Vision
        {
            readonly get => vision;
            set => vision = value;
        }

        public int GrindMiniGameBallSize
        {
            readonly get => grindMiniGameBallSize;
            set => grindMiniGameBallSize = value;
        }

        public int GrindLeniency
        {
            readonly get => grindLeniency;
            set => grindLeniency = value;
        }

        public int ScoreMultiplier
        {
            readonly get => scoreMultiplier;
            set => scoreMultiplier = value;
        }

        public Stats Add(IStats stats)
        {
            //adds bonus stats
            var newStats = new Stats();
            newStats.GrindMiniGameBallSize += stats.GrindMiniGameBallSize;
            newStats.Stability += stats.Stability;
            newStats.CoffinTimeAdded += stats.CoffinTimeAdded;
            newStats.Vision += stats.Vision;
            newStats.GrindLeniency += stats.GrindLeniency;
            newStats.ScoreMultiplier += stats.ScoreMultiplier;

            newStats.GrindMiniGameBallSize += GrindMiniGameBallSize;
            newStats.Stability += Stability;
            newStats.CoffinTimeAdded += CoffinTimeAdded;
            newStats.Vision += Vision;
            newStats.GrindLeniency += GrindLeniency;
            newStats.ScoreMultiplier += ScoreMultiplier;

            return newStats;
        }
    }
}