using System;
using UnityEditor.UIElements;
using UnityEngine;

namespace Hero
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float jumpForce;
        
        private Vector2 direction;
        private Rigidbody2D rb;
        
        [SerializeField] private bool canClimb = false;
        [SerializeField] private bool onFloor = true;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {
            if (direction.x != 0 && onFloor)
            {
                var delta = direction.x * speed * Time.deltaTime;
                var newXPosition = transform.position.x + delta;
                transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
            }
            // Небольшая симуляция лазания по лестнице
            if (direction.y != 0 && canClimb)
            {
                rb.simulated = false;
                onFloor = false;
                var delta = direction.y * speed * Time.deltaTime;
                var newYPosition = transform.position.y + delta;
                transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
            }
                
        }

        public void Jump()
        {
            rb.velocity = new Vector2(direction.x, jumpForce);
        }

        public void SetDirection(Vector2  dir)
        {
            this.direction = dir;
        }

        public void SaySomething()
        {
            Debug.Log("SaySomething");
        }

    }
}