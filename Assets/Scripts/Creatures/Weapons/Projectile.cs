using System;
using UnityEngine;

namespace Creatures.Weapons
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed;

        private Rigidbody2D rb;
        private int direction;
        
        private void Start()
        {
            direction = transform.lossyScale.x > 0 ? 1 : -1;
            rb = GetComponent<Rigidbody2D>();
            var force = new Vector2(direction * speed, 0);
            rb.AddForce(force, ForceMode2D.Impulse);
        }

        //private void FixedUpdate()
        //{
        //   var pos = rb.position;
        //  pos.x += direction * speed * Time.fixedDeltaTime;
        //  rb.MovePosition(pos);
        //}
    }
    
}