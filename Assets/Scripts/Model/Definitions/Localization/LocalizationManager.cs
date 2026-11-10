using System;
using System.Collections.Generic;
using Model.Data.Properties;
using UnityEngine;

namespace Model.Definitions.Localization
{
    public class LocalizationManager
    {
        public static readonly LocalizationManager I;

        private StringPersistantProperty currentLocale = new StringPersistantProperty("en", "localization/current");
        private Dictionary<string, string> localization;
        
        public event Action OnLocaleChanged;
        
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
            var def = Resources.Load<LocaleDef>($"Locales/{localeToLoad}");
            localization = def.GetData();
            OnLocaleChanged?.Invoke();
        }

        public string Localize(string key)
        {
            return localization.TryGetValue(key, out var value) ? value : $"%%%{key}%%%";
        }
    }
}