using System;
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
        [SerializeField] private TMP_Text healthText;

        public int CurrentHealth { get; private set; }

        public int GetMaxHealth()
        {
            return maxHealth;
        }

        private void Awake()
        {
            CurrentHealth = maxHealth;
        }

        private void Update()
        {
            ShowHealth();
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            onTakeDamage?.Invoke();
            if (CurrentHealth <= 0)
                onDie?.Invoke();
        }

        public void TakeHeal(int heal)
        {
            CurrentHealth += heal;
            onTakeHeal?.Invoke();
            if (CurrentHealth > maxHealth)
                CurrentHealth = maxHealth;
        }

        private void ShowHealth()
        {
            healthText.text = $"Health: {CurrentHealth.ToString()} / {maxHealth.ToString()}";
        }
    }
}
