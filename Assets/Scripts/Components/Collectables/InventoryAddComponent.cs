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
        [SerializeField] private bool destroyOnSuccess;

        public void Add(GameObject go)
        {
            var receiver = go.GetInterface<ICanAddInInventory>();
            if (receiver == null || !receiver.TryAddInInventory(id, count)) return;
            if (!destroyOnSuccess) return;

            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
