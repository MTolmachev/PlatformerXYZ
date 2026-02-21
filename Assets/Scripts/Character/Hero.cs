using System;
using Components;
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
        [SerializeField] private float interactionRadius;
        [SerializeField] private LayerMask interactionLayer;
        [SerializeField] private SpawnComponent footStepPerticles;
        [SerializeField] private SpawnComponent jumpAirPerticles;
        [SerializeField] private ParticleSystem hitParticles;

        private readonly Collider2D[] interactionResult = new Collider2D[1];
        private Vector2 direction;
        private Rigidbody2D rb;
        private Animator animator;
        
        private bool canDoubleJump;
        
        private static readonly int IsRunning = Animator.StringToHash("isRunning");
        private static readonly int VerticalVelocity = Animator.StringToHash("verticalVelocity");
        private static readonly int Grounded = Animator.StringToHash("isGrounded");
        private static readonly int Hit = Animator.StringToHash("isHit");

        private int coinsValue = 0;

        public void CollectGold(int amount)
        {
            coinsValue += amount;
            goldText.text = coinsValue.ToString();
        }
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
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
            SpawnJumpAir();
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
                transform.localScale = Vector3.one;
            else if(direction.x < 0)
                transform.localScale = new Vector3(-1, 1, 1);
        }

        public void TakeDamage()
        {
            animator.SetTrigger(Hit);
            rb.velocity = new Vector2(rb.velocity.x, damageJumpForce);
            
            if(coinsValue > 0)
                SpawnCoins();
        }

        public void Interact()
        {
            var size = Physics2D.OverlapCircleNonAlloc(
                transform.position, 
                interactionRadius, 
                interactionResult, 
                interactionLayer);

            for (int i = 0; i < size; i++)
            {
                var interactable = interactionResult[i].GetComponent<InteractableComponent>();
                if (interactable != null)
                    interactable.Interact();
            }
        }

        public void SpawnFootDust()
        {
            footStepPerticles.Spawn();
        }
        
        public void SpawnJumpAir()
        {
            jumpAirPerticles.Spawn();
        }

        private void SpawnCoins()
        {
            var numCoinsToDispose = Mathf.Min(coinsValue, 5);
            coinsValue -= numCoinsToDispose;
            goldText.text = coinsValue.ToString();

            var burst = hitParticles.emission.GetBurst(0);
            burst.count = numCoinsToDispose;
            hitParticles.emission.SetBurst(0, burst);
            
            hitParticles.gameObject.SetActive(true);
            hitParticles.Play();
        }

        
            
    }
}