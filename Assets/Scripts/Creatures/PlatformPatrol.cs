using System;
using System.Collections;
using UnityEngine;

namespace Creatures
{
    public class PlatformPatrol : Patrol
    {
        [SerializeField] private LayerCheck groundCheck;
        [SerializeField] private LayerCheck wallCheck;
        
        private Vector3 direction = new Vector3(1,0,0);
        

        public override IEnumerator DoPatrol()
        {
            while (enabled)
            {
                if (!IsForwardPlatform())
                {
                    direction.x *= -1;
                }
                Creature.SetDirection(direction.normalized);
                yield return new WaitForFixedUpdate();
            }
        }
        
        private bool IsForwardPlatform()
        {
            return groundCheck.IsTouchingLayer & !wallCheck.IsTouchingLayer;
        }
    }
}