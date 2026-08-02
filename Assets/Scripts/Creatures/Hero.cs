using Components;
using Model;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;

namespace Creatures
{
    public class Hero : Creature
    {
        [SerializeField] private TMP_Text goldText;
/*
        [SerializeField] private float interactionRadius;
        [SerializeField] private LayerMask interactionLayer;
*/
        [SerializeField] private ParticleSystem hitParticles;

        [SerializeField] private AnimatorController armed;
        [SerializeField] private AnimatorController unarmed;
        
        [SerializeField] private CheckCircleOverlap interactionCheck;
        private bool canDoubleJump;
        
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

        protected override void Awake()
        {
            base.Awake();
            Animator.runtimeAnimatorController = unarmed;
            
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
            healthComponent.SetMaxHealth(session.Data.maxHp);
            UpdateHeroWeapon();
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
            Rb.velocity = new Vector2(Rb.velocity.x, 0f);
            Rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            SpawnJumpAir();
        }

        public override void TakeDamage()
        {
            base.TakeDamage();
            
            if(session.Data.coins > 0)
                SpawnCoins();
        }

        public void Interact()
        {
            interactionCheck.Check();
        }

        public void SpawnFootDust()
        {
            particles.Spawn("Run");
        }
        
        public void SpawnJumpAir()
        {
            particles.Spawn("Jump");
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
        
        public override void Attack()
        {
            if(!session.Data.isArmed) return;
            base.Attack();
            
        }

        public void ArmHero()
        {
            session.Data.isArmed = true;
            UpdateHeroWeapon();
        }

        private void UpdateHeroWeapon()
        {
            Animator.runtimeAnimatorController = session.Data.isArmed ? armed : unarmed;
        }
    }
}