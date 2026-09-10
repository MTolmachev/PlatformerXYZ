using UnityEngine;
using UnityEngine.Events;

namespace Components.Collectables
{
    public abstract class CollectObjectComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent onCollected;

        public void Collect(GameObject collector)
        {
            if (!TryCollect(collector))
                return;
            onCollected?.Invoke();
        }

        protected abstract bool TryCollect(GameObject collector);
    }
}