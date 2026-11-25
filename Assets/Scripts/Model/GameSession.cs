using System.Collections.Generic;
using System.Linq;
using Components.LevelManagement;
using Model.Data;
using Model.Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils.Disposables;

namespace Model
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;
        [SerializeField] private string defaultCheckPoint;
        public PlayerData Data => playerData;

        private PlayerData _savedData;
        private readonly CompositeDisposable trash =  new CompositeDisposable();
        public QuickInventoryModel QuickInventory { get; private set; }
        public PerksModel PerksModel { get; private set; }
        private bool _isReloading;
        
        private readonly List<string>  checkPoints = new List<string>();

        private void Awake()
        {
            var existsSession = GetExistsSession();
            
            if (existsSession != null)
            {
                existsSession.StartSession(defaultCheckPoint);
                Destroy(gameObject);
                return;
            }
        
            Save();
            InitModels();
            DontDestroyOnLoad(this);
            SceneManager.sceneLoaded += OnSceneLoaded;
            StartSession(defaultCheckPoint);
        }

        private void StartSession(string defaultCheckpoint)
        {
            SetChecked(defaultCheckpoint);
            
            LoadHud();
            SpawnHero();
        }

        private void SpawnHero()
        {
            var checkpoints = FindObjectsOfType<CheckPointComponent>();
            var lastCheckPoint = checkPoints.Last();
            foreach (var checkpoint in checkpoints)
            {
                if (checkpoint.Id != lastCheckPoint) continue;
                checkpoint.SpawnHero();
                break;
            }
        }

        private void InitModels()
        {
            QuickInventory = new QuickInventoryModel(Data);
            trash.Retain(QuickInventory);

            PerksModel = new PerksModel(Data);
            trash.Retain(PerksModel);
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
            trash.Dispose();
            playerData = _savedData.Clone();
            InitModels();
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

        private void Save()
        {
            _savedData = playerData.Clone();
        }

        private GameSession GetExistsSession()
        {
            var sessions = FindObjectsOfType<GameSession>();
            foreach (var session in sessions)
            {
                if (session != this)
                {
                    return session;
                }
            }
            return null;
        }

        public bool IsChecked(string id)
        {
            return checkPoints.Contains(id);
        }
        
        public void SetChecked(string id)
        {
            if (IsChecked(id)) return;
            Save();
            checkPoints.Add(id);
        }
    }
}