using UnityEngine;
using UnityEngine.Events;

namespace Components.Interaction
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
