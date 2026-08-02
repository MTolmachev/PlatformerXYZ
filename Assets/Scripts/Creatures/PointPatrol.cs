using System;
using System.Collections;
using UnityEngine;

namespace Creatures
{
    public class PointPatrol : Patrol
    {
        [SerializeField] private Transform[] points;
        [SerializeField] private float treshold = 1f;
        
        private Creature creature;
        private int destinationPointIndex;

        private void Awake()
        {
            creature = GetComponent<Creature>();
        }

        public override IEnumerator DoPatrol()
        {
            while (enabled)
            {
                if (IsOnPoint())
                {
                    destinationPointIndex = (int)Mathf.Repeat(destinationPointIndex + 1, points.Length);
                }
                
                var direction = points[destinationPointIndex].position -  transform.position;
                direction.y = 0;
                creature.SetDirection(direction.normalized);
                yield return null;
            }
        }

        private bool IsOnPoint()
        {
            return (points[destinationPointIndex].position - transform.position).magnitude < treshold;
        }
    }
}