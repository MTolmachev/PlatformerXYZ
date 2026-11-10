using Model.Data;
using UI.Widgets;
using UnityEngine;

namespace UI.Settings
{
    public class SettingsWindow : AnimatedWindow
    {
        [SerializeField] private AudioSettingsWidget music;
        [SerializeField] private AudioSettingsWidget sfx;
        
        protected override void Start()
        {
            base.Start();
            
            music.SetModel(GameSettings.I.MusicVolume);
            sfx.SetModel(GameSettings.I.SfxVolume);
        }
    }
}