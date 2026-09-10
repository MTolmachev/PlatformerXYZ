using System.Collections;
using UnityEngine;

namespace Creatures.Mobs.Patrolling
{
    public abstract class Patrol  : MonoBehaviour
    {
        public abstract IEnumerator DoPatrol();
        
        protected Creature Creature;

        private void Awake()
        {
            Creature = GetComponent<Creature>();
        }
    }
}