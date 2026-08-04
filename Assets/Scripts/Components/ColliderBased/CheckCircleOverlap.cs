using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Components.ColliderBased
{
    public class CheckCircleOverlap : MonoBehaviour
    {
        [SerializeField] private float radius = 1f;
        [SerializeField] private LayerMask layer;
        [SerializeField] private string[] tags; 
        [SerializeField] private OnOverlapEvent onOverlap;
        
        private readonly Collider2D[] interactionResult = new Collider2D[10];

        private void OnDrawGizmosSelected()
        {
            Handles.color = HandlesUtils.TransparentRed;
            Handles.DrawSolidDisc(transform.position, Vector3.forward, radius);
        }

        public void Check()
        {
            var size = Physics2D.OverlapCircleNonAlloc(
                transform.position, 
                radius, 
                interactionResult,
                layer);

            var overlaps = new List<GameObject>();

            for (var i = 0; i < size; i++)
            {
                var overlapResult = interactionResult[i];
                var isInTags = tags.Any(tag => overlapResult.CompareTag(tag));
                if(isInTags)
                    onOverlap?.Invoke(overlapResult.gameObject);
            }
            
        }
    }
    [Serializable]
    public class OnOverlapEvent : UnityEvent<GameObject>
    {
    
    }
}