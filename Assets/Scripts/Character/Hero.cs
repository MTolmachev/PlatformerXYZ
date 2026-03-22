using System;
using Components;
using Model;
using UnityEditor.UIElements;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEditor.Animations;
using Utils;

namespace Character
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float jumpForce;
        [SerializeField] private float damageJumpForce;
        [SerializeField] private int damage;
        [SerializeField] private LayerCheck layerCheck;
        [SerializeField] private TMP_Text goldText;
        [SerializeField] private float interactionRadius;
        [SerializeField] private LayerMask interactionLayer;
        [SerializeField] private SpawnComponent footStepPerticles;
        [SerializeField] private SpawnComponent jumpAirPerticles;
        [SerializeField] private ParticleSystem hitParticles;

        [SerializeField] private AnimatorController armed;
        [SerializeField] private AnimatorController unarmed;

        [SerializeField] private CheckCircleOverlap attackRange;
        private Vector2 direction;
        private Rigidbody2D rb;
        private Animator animator;
        
        private readonly Collider2D[] interactionResult = new Collider2D[1];
        
        private bool canDoubleJump;
        
        private static readonly int IsRunning = Animator.StringToHash("isRunning");
        private static readonly int VerticalVelocity = Animator.StringToHash("verticalVelocity");
        private static readonly int Grounded = Animator.StringToHash("isGrounded");
        private static readonly int Hit = Animator.StringToHash("isHit");
        private static readonly int Attacking = Animator.StringToHash("attack");

        private GameSession session;

        public int GetCoinsValue()
        {
            return session.Data.coins;
        }

        public void CollectGold(int amount)
        {
            session.Data.coins += amount;
            goldText.text = session.Data.coins.ToString();
        }
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            animator.runtimeAnimatorController = unarmed;
        }

        public void OnHealthChanged(int health)
        {
            session.Data.hp = health;
        }

        private void Start()
        {
            session = FindObjectOfType<GameSession>();
            var healthComponent = GetComponent<HealthComponent>();
            healthComponent.SetHealth(session.Data.hp);
            UpdateHeroWeapon();
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
            
            if(session.Data.coins > 0)
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
            var numCoinsToDispose = Mathf.Min(session.Data.coins, 5);
            session.Data.coins -= numCoinsToDispose;
            goldText.text = session.Data.coins.ToString();

            var burst = hitParticles.emission.GetBurst(0);
            burst.count = numCoinsToDispose;
            hitParticles.emission.SetBurst(0, burst);
            
            hitParticles.gameObject.SetActive(true);
            hitParticles.Play();
        }
        
        public void Attack()
        {
            if(!session.Data.isArmed) return;
            animator.SetTrigger(Attacking);
            
        }

        public void OnAttack()
        {
            var gos = attackRange.GetObjectsInRange();
            foreach (var go in gos)
            {
                var hp = go.GetComponent<HealthComponent>();
                if (hp != null && go.CompareTag("Enemy"))
                    hp.TakeDamage(damage);
            }
        }

        public void ArmHero()
        {
            session.Data.isArmed = true;
            UpdateHeroWeapon();
        }

        private void UpdateHeroWeapon()
        {
            animator.runtimeAnimatorController = session.Data.isArmed ? armed : unarmed;
        }
    }
}