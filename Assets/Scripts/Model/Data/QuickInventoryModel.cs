using System;
using Model.Data.Properties;
using Model.Definitions;
using UnityEngine;
using Utils.Disposables;

namespace Model.Data
{
    public class QuickInventoryModel : IDisposable
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
            var changedItemDef = DefsFacade.I.Items.Get(id);
            if (!changedItemDef.HasTag(ItemTag.Usable)) return;

            var selectedId = TryGetSelectedItem(out var selectedItem) ? selectedItem.Id : null;
            Inventory = data.Inventory.GetAll(ItemTag.Usable);

            if (Inventory.Length == 0)
            {
                SelectedIndex.Value = 0;
            }
            else
            {
                var previousItemIndex = Array.FindIndex(Inventory, item => item.Id == selectedId);
                SelectedIndex.Value = previousItemIndex >= 0
                    ? previousItemIndex
                    : Mathf.Clamp(SelectedIndex.Value, 0, Inventory.Length - 1);
            }

            OnChanged?.Invoke();
        }

        public void SetNextItem()
        {
            if (Inventory.Length <= 1) return;

            SelectedIndex.Value = (int) Mathf.Repeat(SelectedIndex.Value + 1, Inventory.Length);
        }

        public bool TryGetSelectedItem(out InventoryItemData item)
        {
            if (Inventory.Length == 0 || SelectedIndex.Value < 0 || SelectedIndex.Value >= Inventory.Length)
            {
                item = null;
                return false;
            }

            item = Inventory[SelectedIndex.Value];
            return true;
        }

        public void Dispose()
        {
            data.Inventory.OnChanged -= OnChangedInventory;
        }
    }
}
