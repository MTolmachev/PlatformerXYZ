using System;
using Model.Data.Properties;
using Model.Definitions;
using UnityEngine;
using Utils.Disposables;

namespace Model.Data
{
    public class QuickInventoryModel
    {
        private readonly PlayerData data;
        
        public InventoryItemData[] Inventory {get; private set;}
        
        public readonly IntProperty SelectedIndex =  new IntProperty();

        public event Action OnChanged;

        public QuickInventoryModel(PlayerData data)
        {
            this.data = data;

            Inventory = this.data.Inventory.GetAll(ItemTag.Usable);
            data.Inventory.OnChanged += OnChangedInventory;
        }

        public IDisposable Subscribe(Action call)
        {
            OnChanged += call;
            return new ActionDisposable(() => OnChanged -= call);
        }

        private void OnChangedInventory(string id, int value)
        {
            var indexFound = Array.FindIndex(Inventory, item => item.Id == id);
            if (indexFound == -1) return;
            Inventory = data.Inventory.GetAll(ItemTag.Usable);
            SelectedIndex.Value = Mathf.Clamp(SelectedIndex.Value, 0, Inventory.Length - 1);
            OnChanged?.Invoke();
        }

        public void SetNextItem()
        {
            SelectedIndex.Value = (int) Mathf.Repeat(SelectedIndex.Value + 1, Inventory.Length);
        }
    }
}