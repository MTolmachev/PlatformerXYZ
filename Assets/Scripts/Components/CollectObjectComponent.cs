using System;
using UnityEngine;
using Character;

namespace Components
{
    public class CollectObjectComponent : MonoBehaviour
    {
        [SerializeField] private int cost;
        private Hero character;

        private void Start()
        {
            character = FindObjectOfType<Hero>();
        }

        public void Collect()
        {
            character.CollectGold(cost);
        }
    }
}
