using System;
using Model.Data.Properties;
using UnityEngine;

namespace Model.Data
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private InventoryData inventory;
        
        public InventoryData Inventory => inventory;
        
        public IntProperty Hp = new IntProperty();
        
        public PlayerData Clone()
        {
            var json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(json);
        }
    }
}