using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Model.Definitions.Localization
{
    [CreateAssetMenu(fileName = "LocaleDef", menuName = "Defs/LocaleDef")]
    public class LocaleDef : ScriptableObject
    {
        [SerializeField] private string url;
        [SerializeField] private List<LocaleItem> localeItems;

        private UnityWebRequest request;

        public Dictionary<string, string> GetData()
        {
            var dict = new Dictionary<string, string>();
            foreach (var localeItem in localeItems)
            {
                dict.Add(localeItem.Key, localeItem.Value);
            }
            return dict;
        }
        
        [ContextMenu("Update locale")]
        public void UpdateLocale()
        {
            if(request != null) return;
            
            request = UnityWebRequest.Get(url);
            request.SendWebRequest().completed += OnDataLoaded;

        }

        private void OnDataLoaded(AsyncOperation operation)
        {
            if (operation.isDone)
            {
                var rows = request.downloadHandler.text.Split('\n');

                foreach (var row in rows)
                {
                    AddLocaleItem(row);
                }
            }
        }

        private void AddLocaleItem(string row)
        {
            try
            {
                var parts = row.Split('\t');
                localeItems.Add(new LocaleItem(key: parts[0], value: parts[1]));
            }
            catch (Exception e)
            {
                Debug.LogError($"Can't parse row {row}. \n {e}");
                throw;
            }
        }


        [Serializable]
        private class LocaleItem
        {
            public string Key;
            public string Value;

            public LocaleItem(string key, string value)
            {
                Key = key;
                Value = value;
            }
        }
    }
}