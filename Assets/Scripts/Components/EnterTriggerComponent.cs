using System;
using UnityEngine;
using UnityEngine.Events;

namespace Components
{
    public class EnterTriggerComponent : MonoBehaviour
    {
        [SerializeField] private string tag;
        [SerializeField] private UnityEvent action;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(tag))
            {
                action?.Invoke();
            }
        }
    }
}
