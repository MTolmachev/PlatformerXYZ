using System;
using Components.LevelManagement;
using Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.PauseMenu
{
    [RequireComponent(typeof(ReloadLevelComponent))]
    public class PauseMenuWindow : AnimatedWindow
    {
        private float defaultTimeScale;

        protected override void Start()
        {
            base.Start();

            defaultTimeScale = Time.timeScale;
            Time.timeScale = 0f;
        }

        public void OnShowSettings()
        {
            //var window = Resources.Load<GameObject>("UI/SettingsWindow");
            var window = Resources.Load<GameObject>("UI/ManagePerksWindow");
            var canvas = FindObjectOfType<Canvas>();
            Instantiate(window, canvas.transform);
        }
        
        public void OnExit()
        {
            var session = FindObjectOfType<GameSession>();
            if (session != null)
                Destroy(session.gameObject);

            SceneManager.LoadScene("MainMenu");
        }

        private void OnDestroy()
        {
            Time.timeScale = defaultTimeScale;
        }
    }
}
