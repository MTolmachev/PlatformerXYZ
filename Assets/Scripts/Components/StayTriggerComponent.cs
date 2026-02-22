using System;
using Misc;
using UnityEngine;
using UnityEngine.Events;

namespace Components
{
    public class StayTriggerComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent action;

        private Collider2D currentActivator;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(currentActivator != null) return;
            
            if (other.gameObject.GetComponent<Pressable>() != null)
            {
                action?.Invoke();
                currentActivator = other;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other == currentActivator)
            {
                currentActivator = null;
                action?.Invoke();
            }
        }
    }
}