using System.Collections.Generic;
using Model;
using Model.Data;
using UnityEngine;

namespace Components.Collectables
{
    public class CollectorComponent : MonoBehaviour, ICanAddInInventory
    {
        [SerializeField] private List<InventoryItemData> items = new List<InventoryItemData>();

        public void AddInInventory(string id, int value)
        {
            items.Add(new InventoryItemData(id) { Value = value });
        }

        public void DropInInventory()
        {
            var session = FindObjectOfType<GameSession>();
            foreach (var inventoryItemData in items)
            {
                session.Data.Inventory.Add(inventoryItemData.Id, inventoryItemData.Value);
            }

            items.Clear();
        }
    }
}
