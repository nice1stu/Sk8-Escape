using UnityEngine;
 using UnityEngine.Serialization;
 
 namespace AudioSettingInterface
 {
     [System.Serializable]
     public class AudioSettings : IAudioSettings
     {
         [SerializeField] private AudioChannelSettings music;
         [SerializeField] private AudioChannelSettings sfx;
 
         public IAudioChannelSettings Music => music;
         public IAudioChannelSettings SFX => sfx;
     }
 }