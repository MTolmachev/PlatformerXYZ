using System;
using System.Collections.Generic;
using System.Linq;
using Model.Definitions;
using UnityEngine;

namespace Model.Data
{
    [Serializable]
    public class InventoryData
    {
        [SerializeField] private List<InventoryItemData> inventory = new List<InventoryItemData>();

        public delegate void OnInventoryChanged(string id, int value);
        
        public OnInventoryChanged OnChanged;
        
        public void Add(string id, int value)
        {
            if(value <= 0) return;

            var itemDef = DefsFacade.I.Items.Get(id);
            if(itemDef.IsVoid) return;

            var item = GetItem(id);

            if (item == null)
            {
                item = new InventoryItemData(id);
                inventory.Add(item);
                item.Weight = itemDef.Weight;
            }
            
            item.Value += value;
            
            OnChanged?.Invoke(id, Count(id));
        }

        public void Remove(string id, int value)
        {
            var itemDef = DefsFacade.I.Items.Get(id);
            if(itemDef.IsVoid) return;
            
            var item = GetItem(id);
            if(item == null) return;
            
            item.Value -= value;
            
            if(item.Value <= 0)
                inventory.Remove(item);
            
            OnChanged?.Invoke(id, Count(id));
        }

        private InventoryItemData GetItem(string id)
        {
            return inventory.FirstOrDefault(item => item.Id == id);
        }

        public int Count(string id)
        {
            var count = 0;
            foreach (var item in inventory)
            {
                if(item.Id == id) count += item.Value;
            }
            return count;
        }

        public float WeightCount()
        {
            return inventory.Sum(item => item.Weight *  item.Value);
        }
    }

    [Serializable]
    public class InventoryItemData
    {
        [InventoryId]public string Id;
        public float Weight;
        public int Value;
        
        //public InventoryItemData() { }
        public InventoryItemData(string id)
        {
            Id = id;
        }
    }
}