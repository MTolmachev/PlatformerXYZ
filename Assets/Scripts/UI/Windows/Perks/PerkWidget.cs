using System;
using Model;
using Model.Definitions.Repositories;
using UI.Widgets;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows.Perks
{
    public class PerkWidget : MonoBehaviour, IItemRenderer<PerkDef>
    {
        [SerializeField] private Image icon;
        [SerializeField] private GameObject isLocked;
        [SerializeField] private GameObject isUsed;
        [SerializeField] private GameObject isSelected;

        private GameSession session;
        private PerkDef data;

        private void Start()
        {
            session = FindObjectOfType<GameSession>();
            UpdateView();
        }

        public void SetData(PerkDef data, int index)
        {
            this.data = data;

            if (session)
                UpdateView();
        }

        private void UpdateView()
        {
            icon.sprite = data.Icon;
            isUsed.SetActive(session.PerksModel.IsUsed(data.Id));
            isSelected.SetActive(session.PerksModel.InterfaceSelection.Value == data.Id);
            isLocked.SetActive(!session.PerksModel.IsUnlocked(data.Id));
        }

        public void OnSelect()
        {
            session.PerksModel.InterfaceSelection.Value = data.Id;
        }
    }
}