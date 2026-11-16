using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Definitions.Localization
{
    public static class LocaleDef
    {
        public static Dictionary<string, string> FromJson(string json)
        {
            var localeFile = JsonUtility.FromJson<LocaleFile>(json);
            var localization = new Dictionary<string, string>();

            foreach (var item in localeFile.items)
            {
                localization.Add(item.key, item.value);
            }

            return localization;
        }

        [Serializable]
        private class LocaleFile
        {
            public LocaleItem[] items;
        }

        [Serializable]
        private class LocaleItem
        {
            public string key;
            public string value;
        }
    }
}
