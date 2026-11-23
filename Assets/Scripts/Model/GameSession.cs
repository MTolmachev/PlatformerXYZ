using Model.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils.Disposables;

namespace Model
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;
        public PlayerData Data => playerData;

        private PlayerData _savedData;
        private readonly CompositeDisposable trash =  new CompositeDisposable();
        public QuickInventoryModel QuickInventory { get; private set; }
        private bool _isReloading;

        private void Awake()
        {
            LoadHud();
            if (IsSessionExist())
            {
                Destroy(gameObject);
                return;
            }
        
            _savedData = playerData.Clone();
            InitModels();
            DontDestroyOnLoad(this);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void InitModels()
        {
            QuickInventory = new QuickInventoryModel(Data);
            trash.Retain(QuickInventory);
        }

        private void LoadHud()
        {
            SceneManager.LoadScene("Hud",  LoadSceneMode.Additive);
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            trash.Dispose();
        }

        public void ResetToLevelStart()
        {
            _isReloading = true;
            playerData = _savedData.Clone();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (_isReloading)
            {
                _isReloading = false;
            }
            else
            {
                _savedData = playerData.Clone();
            }
        }

        private bool IsSessionExist()
        {
            var sessions = FindObjectsOfType<GameSession>();
            foreach (var session in sessions)
            {
                if (session != this)
                {
                    Debug.Log(session.gameObject.name);
                    return true;
                }
            }
            return false;
        }
    }
}