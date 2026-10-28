using System;
using UnityEngine;

namespace Model.Definitions
{
    [CreateAssetMenu(menuName = "Defs/InventoryItems", fileName = "InventoryItems")]
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
        [SerializeField] private bool isStackable;
        [SerializeField] private Sprite icon;
        
        public string Id => id;
        public bool IsStackable => isStackable;
        
        public bool IsVoid => string.IsNullOrEmpty(id);
        public Sprite Icon => icon;
    }
}
