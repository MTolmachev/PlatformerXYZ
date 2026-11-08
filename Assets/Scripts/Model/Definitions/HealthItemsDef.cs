using System;
using UnityEngine;

namespace Model.Definitions
{
    [CreateAssetMenu(menuName = "Defs/HealthItems", fileName = "HealthItems")]
    public class HealthItemsDef : ScriptableObject
    {
        [SerializeField] private HealthItemDef[] items;

        public HealthItemDef Get(string id)
        {
            foreach (var item in items)
            {
                if (item.Id == id)
                    return item;
            }

            return default;
        }
    }

    [Serializable]
    public struct HealthItemDef
    {
        [InventoryId] [SerializeField] private string id;
        [Min(1)] [SerializeField] private int healAmount;

        public string Id => id;
        public int HealAmount => healAmount;
        public bool IsVoid => string.IsNullOrEmpty(id);
    }
}
