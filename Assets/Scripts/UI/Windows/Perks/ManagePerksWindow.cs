using System;
using Model;
using Model.Definitions;
using Model.Definitions.Localization;
using Model.Definitions.Repositories;
using UI.Widgets;
using UnityEngine;
using UnityEngine.UI;
using Utils.Disposables;

namespace UI.Windows.Perks
{
    public class ManagePerksWindow : AnimatedWindow
    {
        [SerializeField] private Button buyButton;
        [SerializeField] private Button useButton;
        [SerializeField] private ItemWidget price;
        [SerializeField] private Text info;
        [SerializeField] private Transform container;

        private PredefinedDataGroup<PerkDef, PerkWidget> dataGroup;
        private readonly CompositeDisposable trash =  new CompositeDisposable();

        private GameSession session;

        protected override void Start()
        {
            base.Start();
            
            dataGroup = new PredefinedDataGroup<PerkDef, PerkWidget>(container);
            session = FindObjectOfType<GameSession>();
            
            trash.Retain(session.PerksModel.Subscribe(OnPerksChanged));
            trash.Retain(buyButton.onClick.Subscribe(OnBuy));
            trash.Retain(useButton.onClick.Subscribe(OnUse));
            
            OnPerksChanged();
        }

        private void OnPerksChanged()
        {
            dataGroup.SetData(DefsFacade.I.Perks.All);
            var selected = session.PerksModel.InterfaceSelection.Value;
            
            useButton.gameObject.SetActive(session.PerksModel.IsUnlocked(selected));
            useButton.interactable = session.PerksModel.Used != selected;
            
            buyButton.gameObject.SetActive(!session.PerksModel.IsUnlocked(selected));
            buyButton.interactable = session.PerksModel.CanBuy(selected);
            
            var def = DefsFacade.I.Perks.Get(selected);
            price.SetData(def.Price);

            info.text = LocalizationManager.I.Localize(def.Info);
        }

        private void OnUse()
        {
            var selected = session.PerksModel.InterfaceSelection.Value;
            session.PerksModel.UsePerk(selected);
        }

        private void OnBuy()
        {
            var selected = session.PerksModel.InterfaceSelection.Value;
            session.PerksModel.Unlock(selected);
        }

        private void OnDestroy()
        {
            trash.Dispose();
        }
    }
}