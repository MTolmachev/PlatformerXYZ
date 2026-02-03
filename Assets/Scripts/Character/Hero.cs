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
        [SerializeField] private float damageJumpForce;
        [SerializeField] private LayerCheck layerCheck;
        [SerializeField] private TMP_Text goldText;

        private Vector2 direction;
        private Rigidbody2D rb;
        private SpriteRenderer sr;
        private Animator animator;
        
        private bool canDoubleJump;
        
        private static readonly int IsRunning = Animator.StringToHash("isRunning");
        private static readonly int VerticalVelocity = Animator.StringToHash("verticalVelocity");
        private static readonly int Grounded = Animator.StringToHash("isGrounded");
        private static readonly int Hit = Animator.StringToHash("isHit");

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
            animator = GetComponent<Animator>();
        }
        
        
        private void FixedUpdate()
        {
            rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

            UpdateSpriteDirection();
            
            animator.SetBool(Grounded, IsGrounded());
            animator.SetFloat(VerticalVelocity, rb.velocity.y);
            animator.SetBool(IsRunning, direction.x != 0);
        }

        public void Jump()
        {
            if (IsGrounded())
            {
                MakeJump();
                canDoubleJump = true;
            }
            else if (canDoubleJump)
            {
                MakeJump();
                canDoubleJump = false;
            }
                
            
        }

        private void MakeJump()
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
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

        private void UpdateSpriteDirection()
        {
            if(direction.x > 0)
                sr.flipX = false;
            else if(direction.x < 0)
                sr.flipX = true;
        }

        public void TakeDamage()
        {
            animator.SetTrigger(Hit);
            rb.velocity = new Vector2(rb.velocity.x, damageJumpForce);
        }
            
    }
}