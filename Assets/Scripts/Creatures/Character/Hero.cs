using System;
using Components;
using Components.ColliderBased;
using Components.GOBased;
using Components.Health;
using Model;
using Model.Data;
using Model.Definitions;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;
using Utils;

namespace Creatures.Character
{
    public class Hero : Creature, ICanAddInInventory
    {
        private static readonly int ThrowKey = Animator.StringToHash("throw");

        [SerializeField] private ParticleSystem hitParticles;
        [SerializeField] private CheckCircleOverlap interactionCheck;
        
        [Header("Animation")]
        [SerializeField] private AnimatorController armed;
        [SerializeField] private AnimatorController unarmed;
        
        [Header("Swords")]
        [SerializeField] private Cooldown throwCooldown;
        
        
        [SerializeField] private SpawnComponent throwSpawner;
        
        private bool canDoubleJump;
        private bool isLastSwordThrowing;
        
        private GameSession session;
        private HealthComponent healthComponent;

        private const string SwordId = "Sword";
        
        private int SwordCount => session.Data.Inventory.Count(SwordId);
        private int CoinsCount => session.Data.Inventory.Count("Coin");

        private string SelectedId => session.QuickInventory.SelectedItem.Id;
        private bool CanThrow
        {
            get
            {
                var def = DefsFacade.I.Items.Get(SelectedId);
                return def.HasTag(ItemTag.Throwable);
            }
        }
        
        private void Start()
        {
            var maxHealth = DefsFacade.I.Player.MaxHealth;
            
            session = FindObjectOfType<GameSession>();
            session.Data.Inventory.OnChanged += OnInventoryChanged;
            healthComponent = GetComponent<HealthComponent>();
            healthComponent.SetMaxHealth(maxHealth);
            healthComponent.SetHealth(session.Data.Hp.Value);
            
            UpdateHeroWeapon();
        }

        private void OnDestroy()
        {
            session.Data.Inventory.OnChanged -= OnInventoryChanged;
        }

        private void OnInventoryChanged(string id, int value)
        {
            if(id == SwordId)
                UpdateHeroWeapon();
        }
        

        public void AddInInventory(string id, int value)
        {
            session.Data.Inventory.Add(id, value);
        }

        public int GetCoinsValue()
        {
            return CoinsCount;
        }

        public void CollectGold(int amount)
        {
            session.Data.Inventory.Add("Coin", amount);
        }
        
        public void OnHealthChanged(int health)
        {
            session.Data.Hp.Value = health;
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
            
            if(CoinsCount > 0)
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
            Sounds.Play("Jump");
            particles.Spawn("Jump");
        }

        private void SpawnCoins()
        {
            var numCoinsToDispose = Mathf.Min(CoinsCount, 5);
            session.Data.Inventory.Remove("Coin", numCoinsToDispose);

            var burst = hitParticles.emission.GetBurst(0);
            burst.count = numCoinsToDispose;
            hitParticles.emission.SetBurst(0, burst);
            
            hitParticles.gameObject.SetActive(true);
            hitParticles.Play();
        }
        

        
        
        public override void Attack()
        {
            if(SwordCount <= 0) return;
            base.Attack();
            
        }

        private void UpdateHeroWeapon()
        {
            var numSwords = session.Data.Inventory.Count(SwordId);
            if(isLastSwordThrowing) return;
            Animator.runtimeAnimatorController = SwordCount > 0 ? armed : unarmed;
        }

        public void OnDoThrow()
        {
            Sounds.Play("Range");
            var throwableId = session.QuickInventory.SelectedItem.Id;
            var throwableDef = DefsFacade.I.ThrowableItems.Get(throwableId);
            
            throwSpawner.SetPrefab(throwableDef.Projectile);
            throwSpawner.Spawn();
            
            session.Data.Inventory.Remove(throwableId, 1);
            
            isLastSwordThrowing = false;
            UpdateHeroWeapon();
        }
        

        public void Throw()
        {
            if (!throwCooldown.IsReady || !CanThrow) return;
            Animator.SetTrigger(ThrowKey);
                
            throwCooldown.Reset();
        }

        public void UsePotion()
        {
            var itemsCount = session.Data.Inventory.Count("HealthPotion");
            if (itemsCount <= 0) return;
            healthComponent.TakeHeal(5);
            session.Data.Inventory.Remove("HealthPotion", 1);
        }

        public void MakePause()
        {
            PauseComponent.Pause();
        }

        public void NextItem()
        {
            session.QuickInventory.SetNextItem();
        }
    }
}
