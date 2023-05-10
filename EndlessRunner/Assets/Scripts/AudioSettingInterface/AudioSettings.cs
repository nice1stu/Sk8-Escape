using UnityEngine;

namespace AudioSettingInterface
 {
     [System.Serializable]
     public struct AudioSettings : IAudioSettings
     {
         [SerializeField] private AudioChannelSettings music;
         [SerializeField] private AudioChannelSettings sfx;

         public IAudioChannelSettings Music => music;
         public IAudioChannelSettings Sfx => sfx;
     }
 }