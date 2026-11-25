using System;
using System.Collections.Generic;
using Model.Data.Properties;
using UnityEngine;

namespace Model.Data
{
    [Serializable]
    public class PerksData
    {
        [SerializeField] private StringProperty used =  new StringProperty();
        [SerializeField] private List<string> unlocked;
        
        public StringProperty Used => used;

        public void AddPerk(string id)
        {
            if(!unlocked.Contains(id))
                unlocked.Add(id);
        }

        public bool IsUnlocked(string id)
        {
            return unlocked.Contains(id);
        }
    }
}