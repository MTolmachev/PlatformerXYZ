using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Utils;

public class CheckCircleOverlap : MonoBehaviour
    {
        [SerializeField] private float radius = 1f;
        
        private readonly Collider2D[] interactionResult = new Collider2D[5];
        
        public GameObject[] GetObjectsInRange()
        {
            var size = Physics2D.OverlapCircleNonAlloc(
                transform.position, 
                radius, 
                interactionResult);

            var overlaps = new List<GameObject>();

            for (var i = 0; i < size; i++)
            {
                overlaps.Add(interactionResult[i].gameObject);
            }
            
            return overlaps.ToArray();
        }

        private void OnDrawGizmosSelected()
        {
            Handles.color = HandlesUtils.TransparentRed;
            Handles.DrawSolidDisc(transform.position, Vector3.forward, radius);
        }
    }