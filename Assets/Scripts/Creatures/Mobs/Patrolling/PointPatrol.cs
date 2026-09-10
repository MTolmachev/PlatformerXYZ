using System.Collections;
using UnityEngine;

namespace Creatures.Mobs.Patrolling
{
    public class PointPatrol : Patrol
    {
        [SerializeField] private Transform[] points;
        [SerializeField] private float threshold = 1f;
        
        private int destinationPointIndex;

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
                Creature.SetDirection(direction.normalized);
                yield return null;
            }
        }

        private bool IsOnPoint()
        {
            return (points[destinationPointIndex].position - transform.position).magnitude < threshold;
        }
    }
}