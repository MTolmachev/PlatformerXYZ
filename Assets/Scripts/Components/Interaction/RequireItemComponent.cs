using Model;
using Model.Data;
using Model.Definitions;
using UnityEngine;
using UnityEngine.Events;

namespace Components.Interaction
{
    public class RequireItemComponent : MonoBehaviour
    {
        [SerializeField] private InventoryItemData[] requiredItems;
        [SerializeField] private bool removeAfterUse;
        
        [SerializeField] private UnityEvent onSuccess;
        [SerializeField] private UnityEvent onFail;
        
        public void Check()
        {
            var session = FindObjectOfType<GameSession>();
            var areAllRequiredItemsMet = true;
            foreach (var item in requiredItems)
            {
                var numItems = session.Data.Inventory.Count(item.Id);
                if(numItems < item.Value)
                    areAllRequiredItemsMet = false;
            }
            if (areAllRequiredItemsMet)
            {
                if (removeAfterUse)
                {
                    foreach (var item in requiredItems)
                    {
                        session.Data.Inventory.Remove(item.Id, item.Value);
                    }
                }
                onSuccess?.Invoke();
            }
            else
            {
                onFail?.Invoke();
            }
        }
    }
}