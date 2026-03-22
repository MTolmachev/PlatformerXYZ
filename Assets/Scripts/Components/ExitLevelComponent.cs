using UnityEngine;
using UnityEngine.SceneManagement;

namespace Components
{
    public class ExitLevelComponent : MonoBehaviour
    {
        [SerializeField]  private string sceneName;
        
        public void ExitLevel()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(sceneName);
        }
    }
}