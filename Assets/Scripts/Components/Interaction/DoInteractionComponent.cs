using UnityEngine;

namespace Components.Interaction
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