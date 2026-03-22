using System;
using Model;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Components
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int maxHealth;
        [SerializeField] private UnityEvent onTakeDamage;
        [SerializeField] private UnityEvent onTakeHeal;
        [SerializeField] private UnityEvent onDie;
        [SerializeField] private HealthChangeEvent onChange;
        [SerializeField] private TMP_Text healthText;

        [SerializeField] private bool canTakeDamage = true;
        
        private GameSession session;
        
        public int CurrentHealth { get; private set; }

        public int GetMaxHealth()
        {
            return maxHealth;
        }

        private void Start()
        {
            session = FindObjectOfType<GameSession>();
            CurrentHealth = session.Data.hp;
            ShowHealth();

        }

        private void Update()
        {
            ShowHealth();
        }
        
        public void TakeDamage(int damage)
        {
            if(!canTakeDamage) return;
            CurrentHealth -= damage;
            onChange?.Invoke(CurrentHealth);
            onTakeDamage?.Invoke();
            if (CurrentHealth <= 0)
                onDie?.Invoke();
        }

        public void TakeHeal(int heal)
        {
            CurrentHealth += heal;
            onChange?.Invoke(CurrentHealth);
            onTakeHeal?.Invoke();
            if (CurrentHealth > maxHealth)
                CurrentHealth = maxHealth;
        }

        private void ShowHealth()
        {
            if(!healthText) return; 
            healthText.text = $"Health: {CurrentHealth.ToString()} / {maxHealth.ToString()}";
        }
        public void SetHealth(int dataHp)
        {
            CurrentHealth = dataHp;
        }
        
        [Serializable]
        public class HealthChangeEvent : UnityEvent<int>
        {
            
        }
    }
}
