using System.Collections;
using UnityEngine;

namespace Creatures
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