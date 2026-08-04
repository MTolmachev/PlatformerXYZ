using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Components.Health
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
        [SerializeField] private float immunityDuration = 1f;
        
        
        public int CurrentHealth { get; private set; }

        private void Awake()
        {
            CurrentHealth = maxHealth;
        }

        public int GetMaxHealth()
        {
            return maxHealth;
        }
        
        public void SetMaxHealth(int hp)
        {
            maxHealth = hp;
            ShowHealth();
        }
        
        public void TakeDamage(int damage)
        {
            if(!canTakeDamage) return;
            if(CurrentHealth <= 0) return;
            CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
            onChange?.Invoke(CurrentHealth);
            onTakeDamage?.Invoke();
            if (CurrentHealth <= 0)
                onDie?.Invoke();
            ShowHealth();
            if (!gameObject) return;
            StartCoroutine(DamageImmunity(immunityDuration));
        }

        public void TakeHeal(int heal)
        {
            CurrentHealth = Mathf.Min(CurrentHealth + heal, maxHealth);;
            onChange?.Invoke(CurrentHealth);
            onTakeHeal?.Invoke();
            ShowHealth();
        }

        private void ShowHealth()
        {
            if(!healthText) return; 
            healthText.text = $"Health: {CurrentHealth.ToString()} / {maxHealth.ToString()}";
        }
        public void SetHealth(int dataHp)
        {
            CurrentHealth = dataHp;
            ShowHealth();
        }

        private IEnumerator DamageImmunity(float duration)
        {
            canTakeDamage = false;
            yield return new WaitForSeconds(duration);
            canTakeDamage = true;
        }
        
        [Serializable]
        public class HealthChangeEvent : UnityEvent<int>
        {
            
        }

        public bool TryHeal(int amount)
        {
            if(amount <= 0) return false;
            if(CurrentHealth >= maxHealth) return false;
            CurrentHealth = Mathf.Min(CurrentHealth + amount, maxHealth);
            
            onChange?.Invoke(CurrentHealth);
            onTakeHeal?.Invoke();
            ShowHealth();
            
            return true;
        }
    }
}
