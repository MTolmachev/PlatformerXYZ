using System;
using Model.Data;
using Model.Data.Properties;
using UnityEngine;

namespace Components.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSettingComponent : MonoBehaviour
    {
        [SerializeField] private SoundSettingType mode;

        private AudioSource source;
        private FloatPersistentProperty model;
        
        private void Start()
        {
            source = GetComponent<AudioSource>();
            model = FindProperty();
            model.onChanged += OnSoundSettingChanged;
            OnSoundSettingChanged(model.Value, model.Value);
        }

        private void OnSoundSettingChanged(float newValue, float oldValue)
        {
            source.volume = newValue;
        }

        private FloatPersistentProperty FindProperty()
        {
            switch (mode)
            {
                case SoundSettingType.Music:
                    return GameSettings.I.MusicVolume;
                case SoundSettingType.Sfx:
                    return GameSettings.I.SfxVolume;
                default:
                    throw new ArgumentException("Undefined mode");
            }

            
        }
    }
}