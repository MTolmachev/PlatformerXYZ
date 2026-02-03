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
        [SerializeField] private UnityEvent onDie;
        [SerializeField] private TMP_Text healthText;
        
        private int currentHealth;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        private void Update()
        {
            ShowHealth();
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            onTakeDamage?.Invoke();
            if (currentHealth <= 0)
                onDie?.Invoke();
        }

        private void ShowHealth()
        {
            healthText.text = $"Health: {currentHealth.ToString()} / {maxHealth.ToString()}";
        }
    }
}
