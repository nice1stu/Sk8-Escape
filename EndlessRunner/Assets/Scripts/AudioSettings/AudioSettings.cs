using UnityEngine;

namespace AudioSettings
 {
     [System.Serializable]
     public struct AudioSettings : IAudioSettings
     {
         [SerializeField] private AudioChannelSettings music;
         [SerializeField] private AudioChannelSettings sfx;

         public IAudioChannelSettings Music => music;
         public IAudioChannelSettings Sfx => sfx;
         
         [SerializeField] private bool _isMuted;
         public bool IsMuted
         {
             get => _isMuted;
             set
             {
                 _isMuted = value;
                 music.Muted = _isMuted;
                 sfx.Muted = _isMuted;
             }
         }

     }
 }