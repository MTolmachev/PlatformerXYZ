using UnityEngine;
using UnityEngine.SceneManagement;
using Character;

namespace Components
{
    public class ReloadLevelComponent : MonoBehaviour
    {
        public void Reload()
        {
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
            
        }
    }
}
