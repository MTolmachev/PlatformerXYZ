using System;
using System.Collections.Generic;
using Model.Data.Properties;
using UnityEngine;

namespace Model.Definitions.Localization
{
    public class LocalizationManager
    {
        private const string DefaultLocale = "en";

        public static readonly LocalizationManager I;

        private StringPersistantProperty currentLocale = new StringPersistantProperty(DefaultLocale, "localization/current");
        private Dictionary<string, string> localization;
        
        public event Action OnLocaleChanged;
        public string LocaleKey => currentLocale.Value;
        
        static LocalizationManager()
        {
            I = new LocalizationManager();
        }

        public LocalizationManager()
        {
            LoadLocale(currentLocale.Value);
        }

        private void LoadLocale(string localeToLoad)
        {
            var localeFile = Resources.Load<TextAsset>($"Locales/{localeToLoad}");

            if (localeFile == null)
            {
                localeToLoad = DefaultLocale;
                localeFile = Resources.Load<TextAsset>($"Locales/{localeToLoad}");
            }

            localization = LocaleDef.FromJson(localeFile.text);
            currentLocale.Value = localeToLoad;
            OnLocaleChanged?.Invoke();
        }

        public string Localize(string key)
        {
            return localization.TryGetValue(key, out var value) ? value : $"%%%{key}%%%";
        }

        public void SetLocale(string localeKey)
        {
            LoadLocale(localeKey);
        }
    }
}
