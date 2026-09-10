using System;
using UnityEngine;

namespace Creatures.Weapons
{
    public class SinusoidalProjectile : BaseProjectile
    {
        [SerializeField] private float frequency = 1f;
        [SerializeField] private float amplitude = 1f;
        
        private float originalY;
        private float time;

        protected override void Start()
        {
            base.Start();
            originalY = Rb.position.y;
            
        }

        private void FixedUpdate()
        {
            var pos = Rb.position;
            pos.x += Direction * speed * Time.fixedDeltaTime; 
            pos.y = originalY + Mathf.Sin(time * frequency) * amplitude;
            Rb.position = pos;
            time += Time.fixedDeltaTime;
        }
    }
}