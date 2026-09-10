using UnityEngine;

namespace Components.ColliderBased
{
    public class LayerCheck : MonoBehaviour
    {
        [SerializeField] private LayerMask layer;
        [SerializeField] private bool isTouchingLayer;
    
        private new Collider2D collider;

        public bool IsTouchingLayer => isTouchingLayer;

        private void Awake()
        {
            collider = GetComponent<Collider2D>();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            isTouchingLayer = collider.IsTouchingLayers(layer);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            isTouchingLayer = collider.IsTouchingLayers(layer);
        }
    }
}
