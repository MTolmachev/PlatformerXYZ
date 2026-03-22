using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Model
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;
        public PlayerData Data => playerData;

        private PlayerData savedData;
        private bool isReloading;

        private void Awake()
        {
            if (IsSessionExist())
            {
                Destroy(gameObject);
                return;
            }
        
            savedData = playerData.Clone();
            DontDestroyOnLoad(this);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void ResetToLevelStart()
        {
            isReloading = true;
            playerData = savedData.Clone();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (isReloading)
            {
                isReloading = false;
            }
            else
            {
                savedData = playerData.Clone();
            }
        }

        private bool IsSessionExist()
        {
            var sessions = FindObjectsOfType<GameSession>();
            foreach (var session in sessions)
            {
                if(session != this)
                    return true;
            }
            return false;
        }
    }
}