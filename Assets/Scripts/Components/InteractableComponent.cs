using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Components
{
    public class InteractableComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent action;

        public void Interact()
        {
            action?.Invoke();
        }
    }
}
