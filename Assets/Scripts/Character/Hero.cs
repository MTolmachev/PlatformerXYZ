using System;
using UnityEditor.UIElements;
using UnityEngine;
using TMPro;

namespace Character
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float jumpForce;
        [SerializeField] private LayerCheck layerCheck;
        [SerializeField] private TMP_Text goldText;

        private Vector2 direction;
        private Rigidbody2D rb;
        private SpriteRenderer sr;

        private int gold = 0;

        public void CollectGold(int amount)
        {
            gold += amount;
            goldText.text = gold.ToString();
        }
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            sr = GetComponent<SpriteRenderer>();
        }
        private void FixedUpdate()
        {
            rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
            if(direction.x > 0)
                sr.flipX = false;
            else if(direction.x < 0)
                sr.flipX = true;
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