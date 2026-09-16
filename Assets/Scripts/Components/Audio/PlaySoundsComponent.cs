using System;
using UnityEngine;

namespace Components.Audio
{
    public class PlaySoundsComponent : MonoBehaviour
    {
        [SerializeField] private AudioSource source;
        [SerializeField] private AudioData[] clips;

        public void Play(string id)
        {
            foreach (var clip in clips)
            {
                if (clip.Id != id) continue;
                
                source.PlayOneShot(clip.Clip);
                break;
            }
        }
        
        
        
        
        
        
        [Serializable]
        public class AudioData
        {
            [SerializeField] private string id;
            [SerializeField] private AudioClip clip;
            
            public string Id => id;
            public AudioClip Clip => clip;
        }
    }
    
}