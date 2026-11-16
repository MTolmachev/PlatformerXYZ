using System;
using Model.Definitions.Localization;
using UI.Widgets;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Windows.Localization
{
    public class LocaleItemWidget : MonoBehaviour, IItemRenderer<LocaleInfo>
    {
        [SerializeField] private Text text;
        [SerializeField] private GameObject selector;
        [SerializeField] private SelectLocale onSelected;

        private LocaleInfo data;

        private void Start()
        {
            LocalizationManager.I.OnLocaleChanged += UpdateSelection;
        }

        public void SetData(LocaleInfo localeInfo, int index)
        {
            data = localeInfo;
            UpdateSelection();
            text.text = localeInfo.LocaleId.ToUpper();
        }

        private void UpdateSelection()
        {
            selector.SetActive(LocalizationManager.I.LocaleKey == data.LocaleId);
        }

        public void OnSelected()
        {
            onSelected?.Invoke(data.LocaleId);
        }

        private void OnDestroy()
        {
            LocalizationManager.I.OnLocaleChanged -= UpdateSelection;
        }
    }

    [Serializable]
    public class SelectLocale : UnityEvent<string>
    {
    }

    public class LocaleInfo
    {
        public string LocaleId;
    }
}
