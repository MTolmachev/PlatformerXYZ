using Creatures.Character;
using Model.Definitions;
using UnityEngine;
using UnityEngine.Events;

namespace Components.Collectables
{
    public class InventoryAddComponent : MonoBehaviour
    {
        [InventoryId][SerializeField] private string id;
        
        [Min(1)]
        [SerializeField] private int amount;
        
        [SerializeField] private UnityEvent onSuccess;

        public void Add(GameObject go)
        {
            var hero = go.GetComponent<Hero>();
            if (hero == null) return;
            if(hero.AddInInventory(id, amount))
                onSuccess?.Invoke();
        }
    }
}