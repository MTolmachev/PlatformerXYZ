using System;
using UnityEditor.UIElements;
using UnityEngine;

namespace Hero
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float jumpForce;
        [SerializeField] private LayerCheck layerCheck;
        private Vector2 direction;
        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        private void FixedUpdate()
        {
            rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
        }

        public void Jump()
        {
            if(IsGrounded())
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            
        }

        public void SetDirection(Vector2 dir)
        {
            this.direction = dir;
        }

        private bool IsGrounded()
        {
            return layerCheck.isTouchingGround;
        }
    }
}