using UnityEngine;

namespace Components
{
    public class DoInteractionComponent : MonoBehaviour
    {
        public void DoInteraction(GameObject target)
        {
            var interactable = target.GetComponent<InteractableComponent>();
            if (interactable != null) 
                interactable.Interact();
        }
    }
}