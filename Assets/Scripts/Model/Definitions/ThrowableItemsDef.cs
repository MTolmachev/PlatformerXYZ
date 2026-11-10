using System;
using UnityEngine;

namespace Model.Definitions
{
    [CreateAssetMenu(menuName = "Defs/ThrowableItems", fileName = "ThrowableItems")]
    public class ThrowableItemsDef : ScriptableObject
    {
        [SerializeField] private ThrowableDef[] items;

        public ThrowableDef Get(string id)
        {
            foreach (var itemDef in items)
            {
                if (itemDef.Id == id) return itemDef;
            }

            return default;
        }
    }

    [Serializable]
    public class ThrowableDef
    {
        [InventoryId][SerializeField] private string id;
        [SerializeField] private GameObject projectile;
        
        public string Id => id;
        public GameObject Projectile => projectile;
    }
}
        
    
