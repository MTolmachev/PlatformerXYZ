using System;
using Model;
using Model.Definitions;
using UI.Widgets;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Hud
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget healthBar;
        [SerializeField] private Image perkIcon;
        [SerializeField] private Image perkCooldown;

        private GameSession session;
        private void Start()
        {
            session = FindObjectOfType<GameSession>();
            session.Data.Hp.OnChanged += OnHealthChanged;
            OnHealthChanged(session.Data.Hp.Value, 0);
        }

        private void Update()
        {
            if (session == null) return;

            var usedPerk = session.PerksModel.Used;
            var isVisible = !string.IsNullOrEmpty(usedPerk);
            perkIcon.enabled = isVisible;
            perkCooldown.enabled = isVisible;

            if (!isVisible) return;

            var perk = DefsFacade.I.Perks.Get(usedPerk);
            perkIcon.sprite = perk.Icon;
            perkCooldown.sprite = perk.Icon;
            perkCooldown.fillAmount = session.PerksModel.GetCooldownProgress(usedPerk);
        }

        private void OnHealthChanged(int newValue, int oldValue)
        {
            var maxHealth = DefsFacade.I.Player.MaxHealth;
            var value = (float) newValue / maxHealth;
            healthBar.SetProgress(value);
        }

        public void OnSettings()
        {
            Components.PauseComponent.Pause();
        }

        private void UpdateHealthBar()
        {
            
        }

        private void OnDestroy()
        {
            if (session != null)
                session.Data.Hp.OnChanged -= OnHealthChanged;
        }
    }
}
