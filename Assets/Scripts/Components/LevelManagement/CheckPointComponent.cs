using System;
using Components.GOBased;
using Model;
using UnityEngine;
using UnityEngine.Events;

namespace Components.LevelManagement
{
    [RequireComponent(typeof(SpawnComponent))]
    public class CheckPointComponent : MonoBehaviour
    {
        [SerializeField] private string id;
        [SerializeField] private UnityEvent setChecked;
        [SerializeField] private UnityEvent setUnchecked;
        [SerializeField] private SpawnComponent heroSpawner;
        public string Id => id;
        
        private GameSession gameSession;
        

        private void Start()
        {
            gameSession = FindObjectOfType<GameSession>();
            if(gameSession.IsChecked(id))
                setChecked?.Invoke();
            else
                setUnchecked?.Invoke();
        }

        public void Check()
        {
            gameSession.SetChecked(id);
            setChecked?.Invoke();
        }

        public void SpawnHero()
        {
            heroSpawner.Spawn();
        }
        
    }
}