using System;
using UnityEngine;

namespace Model.Data
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private InventoryData inventory;
        
        public InventoryData Inventory => inventory;
        
        public int maxHp;
        public int hp;


        public PlayerData Clone()
        {
            var json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(json);
        }
    }
}