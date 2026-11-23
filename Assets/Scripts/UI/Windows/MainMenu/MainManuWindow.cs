using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Windows.MainMenu
{
    public class MainManuWindow : AnimatedWindow
    {
        private Action closeAction;
        
        public void OnShowSettings()
        {
            var window = Resources.Load<GameObject>("UI/SettingsWindow");
            var canvas = FindObjectOfType<Canvas>();
            Instantiate(window, canvas.transform);
        }

        public void OnLanguages()
        {
            var window = Resources.Load<GameObject>("UI/LocalizationWindow");
            var canvas = FindObjectOfType<Canvas>();
            Instantiate(window, canvas.transform);
        }

        public void OnStartGame()
        {
            closeAction = () =>
            {
                SceneManager.LoadScene("Test");
            };
            Close();
        }

        public void OnExit()
        {
            closeAction = () =>
            {
                Application.Quit();
            
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
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
