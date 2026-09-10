using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Definitions
{
    [CreateAssetMenu(menuName = "Defs/InventoryItems", fileName = "InventoryDef")]
    public class InventoryItemsDef : ScriptableObject
    {
        [SerializeField] private ItemDef[] items;

        public ItemDef Get(string id)
        {
            foreach (var itemDef in items)
            {
                if(itemDef.Id == id) return itemDef;
            } 
            return default;
        }
        
#if UNITY_EDITOR
        public ItemDef[] ItemsForEditor => items;
#endif
        
    }

    [Serializable]
    public struct ItemDef
    {
        [SerializeField] private string id;
        [SerializeField] private float weight;
        
        public string Id => id;
        public float Weight => weight;
        
        public bool IsVoid => string.IsNullOrEmpty(id);
    }
}