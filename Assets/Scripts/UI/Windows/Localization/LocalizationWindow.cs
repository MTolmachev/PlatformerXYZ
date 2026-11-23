using System.Collections.Generic;
using Model.Definitions.Localization;
using UI.Widgets;
using UnityEngine;

namespace UI.Windows.Localization
{
    public class LocalizationWindow : AnimatedWindow
    {
        [SerializeField] private Transform container;
        [SerializeField] private LocaleItemWidget prefab;

        private DataGroup<LocaleInfo, LocaleItemWidget> dataGroup;

        private readonly string[] supportedLocales = {"en", "ru"};

        protected override void Start()
        {
            base.Start();
            dataGroup = new DataGroup<LocaleInfo, LocaleItemWidget>(prefab, container);
            dataGroup.SetData(ComposeData());
        }

        private List<LocaleInfo> ComposeData()
        {
            var data = new List<LocaleInfo>();
            foreach (var locale in supportedLocales)
            {
                data.Add(new LocaleInfo {LocaleId = locale});
            }

            return data;
        }

        public void OnSelected(string selectedLocale)
        {
            LocalizationManager.I.SetLocale(selectedLocale);
        }
    }
}
