using System;
using Model;
using Model.Definitions;
using UI.Widgets;
using UnityEngine;

namespace UI.Hud
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget healthBar;

        private GameSession session;
        private void Start()
        {
            session = FindObjectOfType<GameSession>();
            session.Data.Hp.onChanged += OnHealthChanged;
            OnHealthChanged(session.Data.Hp.Value, 0);
        }

        private void OnHealthChanged(int newValue, int oldValue)
        {
            var maxHealth = DefsFacade.I.Player.MaxHealth;
            var value = (float) newValue / maxHealth;
            healthBar.SetProgress(value);
        }

        private void UpdateHealthBar()
        {
            
        }

        private void OnDestroy()
        {
            session.Data.Hp.onChanged -= OnHealthChanged;
        }
    }
}