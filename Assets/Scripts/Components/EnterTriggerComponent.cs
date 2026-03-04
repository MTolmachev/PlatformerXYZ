using System;
using UnityEngine;
using UnityEngine.Events;

namespace Components
{
    public class EnterTriggerComponent : MonoBehaviour
    {
        [SerializeField] private new string tag;
        [SerializeField] private EnterEvent action;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(tag))
            {
                action?.Invoke(other.gameObject);
            }
        }
    }
    
    [Serializable]
    public class EnterEvent : UnityEvent<GameObject>
    {
            
    }
}
