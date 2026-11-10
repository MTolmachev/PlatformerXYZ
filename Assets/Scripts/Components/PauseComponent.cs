using UI.PauseMenu;
using UnityEngine;

namespace Components
{
    public class PauseComponent : MonoBehaviour
    {
        public static void Pause()
        {
            if (FindObjectsOfType<PauseMenuWindow>().Length != 0) return; 
            Time.timeScale = 0f;
            var window = Resources.Load<GameObject>("UI/PauseMenuWindow");
            var canvas = FindObjectOfType<Canvas>();
            Instantiate(window, canvas.transform);
        }
    }
}