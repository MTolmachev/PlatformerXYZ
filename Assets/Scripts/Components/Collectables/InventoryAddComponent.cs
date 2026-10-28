using Creatures.Character;
using Model.Data;
using Model.Definitions;
using Utils;
using UnityEngine;

namespace Components.Collectables
{
    public class InventoryAddComponent : MonoBehaviour
    {
        [InventoryId][SerializeField] private string id;
        
        [SerializeField] private int count;

        public void Add(GameObject go)
        {
            var receiver = go.GetInterface<ICanAddInInventory>();
            receiver?.AddInInventory(id, count);
        }
    }
}
