using System;
using Model.Definitions.Repositories.Items;
using UnityEngine;

namespace Model.Definitions.Repositories
{
    [CreateAssetMenu(menuName = "Defs/Repository/Perks", fileName = "Perks")]

    public class PerkRepository : DefRepository<PerkDef>
    {
        
    }

    [Serializable]
    public struct PerkDef : IHaveId
    {
        [SerializeField] private string id;
        [SerializeField] private Sprite icon;
        [SerializeField] private string info;
        [SerializeField] private ItemWithCount price;
        [SerializeField] private float cooldown;

        
        public string Id => id;
        public Sprite Icon => icon;
        public string Info => info;
        public ItemWithCount Price => price;
        public float Cooldown => cooldown;
    }

    [Serializable]
    public struct ItemWithCount
    {
        [InventoryId] [SerializeField] private string itemId;
        [SerializeField] private int count;
        
        public string ItemId => itemId;
        public int Count => count;
    }
}
