using System;
using UnityEditor.UIElements;
using UnityEngine;

namespace Hero
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float climbSpeed;
        [SerializeField] private float jumpForce;
        
        private Vector2 direction;
        private Rigidbody2D rb;
        
        [SerializeField] private bool onChain = false;
        [SerializeField] private bool onFloor = true;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {
            if (direction.x != 0)
            {
                var delta = direction.x * speed * Time.deltaTime;
                var newXPosition = transform.position.x + delta;
                transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
            }
            if (direction.y != 0 && onChain)
            {
                var vertical = direction.y;
                rb.velocity = new Vector2(0f, vertical * climbSpeed);
            }
                
        }

        public void Jump()
        {
            rb.velocity = new Vector2(direction.x, jumpForce);
            
            if (onChain)
            {
                onChain = false;
                rb.gravityScale = 1;
                rb.velocity = new Vector2(jumpForce, jumpForce);
            }
        }

        public void SetDirection(Vector2 dir)
        {
            this.direction = dir;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Chain"))
            { 
               onChain = true;
               rb.gravityScale = 0;
               rb.velocity = Vector2.zero;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Chain"))
            {
                onChain = false;
                rb.gravityScale = 1;
            }
        }

        public void SaySomething()
        {
            Debug.Log("SaySomething");
        }

    }
}