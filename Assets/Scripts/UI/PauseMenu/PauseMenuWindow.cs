using System;
using Components.LevelManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.PauseMenu
{
    [RequireComponent(typeof(ReloadLevelComponent))]
    public class PauseMenuWindow : AnimatedWindow
    {
        private Action closeAction;
        
        private ReloadLevelComponent reloadLevel;

        protected override void Start()
        {
            base.Start();
            reloadLevel = GetComponent<ReloadLevelComponent>();
        }

        public void OnShowSettings()
        {
            var window = Resources.Load<GameObject>("UI/SettingsWindow");
            var canvas = FindObjectOfType<Canvas>();
            Instantiate(window, canvas.transform);
        }
        
        public void OnRestartGame()
        {
            closeAction = () =>
            {
                reloadLevel.Reload();
                Time.timeScale = 1f;
            };
            Close();
        }

        public void OnExit()
        {
            closeAction = () =>
            {
                SceneManager.LoadScene("MainMenu");
                Time.timeScale = 1f;
            };
            Close();
        }

        public override void OnCloseAnimationComplete()
        {
            base.OnCloseAnimationComplete();
            closeAction?.Invoke();
        }
    }
}