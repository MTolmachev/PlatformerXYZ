using System;
using Model.Data.Properties;
using UnityEngine;

namespace Model.Data
{
    [CreateAssetMenu(menuName = "Data/GameSettings", fileName = "GameSettings")]
    public class GameSettings : ScriptableObject
    {
        [SerializeField] private FloatPersistentProperty musicVolume;
        [SerializeField] private FloatPersistentProperty sfxVolume;
        
        public FloatPersistentProperty MusicVolume => musicVolume;
        public FloatPersistentProperty SfxVolume => sfxVolume;

        private static GameSettings instance;
        public static GameSettings I => instance == null ? LoadGameSettings() : instance;

        private static GameSettings LoadGameSettings()
        {
           return Resources.Load<GameSettings>("GameSettings");
        }

        private void OnEnable()
        {
            musicVolume = new FloatPersistentProperty(1, nameof(SoundSettingType.Music)); 
            sfxVolume = new FloatPersistentProperty(1, nameof(SoundSettingType.Sfx));
        }

        private void OnValidate()
        {
            musicVolume.Validate();
            sfxVolume.Validate();
        }
    }

    public enum SoundSettingType
    {
        Music,
        Sfx
    }
}