using System;
using System.Collections.Generic;
using Model;
using Model.Data;
using UnityEngine;
using Utils.Disposables;

namespace UI.Hud.QuickInventory
{
    public class QuickInventoryController : MonoBehaviour
    {
        [SerializeField] private Transform container;
        [SerializeField] private InventoryItemWidget prefab;
        
        private readonly CompositeDisposable trash = new CompositeDisposable();
        
        private GameSession session;
        private InventoryItemData[] inventory;
        private List<InventoryItemWidget> createdItems = new List<InventoryItemWidget>();
        
        private void Start()
        {
            session = FindObjectOfType<GameSession>();
            // subscribe model

            Rebuild();
        }

        private void Rebuild()
        {
            inventory = session.Data.Inventory.GetAll();
            
            //create required items
            for (var i = createdItems.Count; i < inventory.Length; i++)
            {
                var item = Instantiate(prefab, container);
                createdItems.Add(item);
            }
            
            // update data and activate

            for (var i = 0; i < inventory.Length; i++)
            {
               createdItems[i].SetData(inventory[i], i);
               createdItems[i].gameObject.SetActive(true);
            }
            
            // hide unused items
            for (var i = inventory.Length; i < createdItems.Count; i++)
            {
                createdItems[i].gameObject.SetActive(false);
            }
        }
    }
}