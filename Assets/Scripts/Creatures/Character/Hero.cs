using System;
using Components.ColliderBased;
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
    public class Hero : Creature
    {
        private static readonly int ThrowKey = Animator.StringToHash("throw");

        [SerializeField] private ParticleSystem hitParticles;
        [SerializeField] private CheckCircleOverlap interactionCheck;
        [Header("Texts")]
        [SerializeField] private TMP_Text goldText;
        [SerializeField] private TMP_Text swordText;
        [SerializeField] private TMP_Text healPotionText;
        [SerializeField] private TMP_Text weightText;
        
        [Header("Animation")]
        [SerializeField] private AnimatorController armed;
        [SerializeField] private AnimatorController unarmed;
        
        [Header("Swords")]
        [SerializeField] private Cooldown throwCooldown;
        [SerializeField] private int maxSwords;
        
        [Header("Inventory")]
        [SerializeField] private int maxWeight;
        
        private bool canDoubleJump;
        private bool isLastSwordThrowing;
        
        private GameSession session;
        private HealthComponent healthComponent;
        
        private int SwordCount => session.Data.Inventory.Count("Sword");
        private int CoinsCount => session.Data.Inventory.Count("Coin");
        private int HealPotionCount => session.Data.Inventory.Count("HealPotion");
        
        private void Start()
        {
            session = FindObjectOfType<GameSession>();
            session.Data.Inventory.OnChanged += OnInventoryChanged;
            healthComponent = GetComponent<HealthComponent>();
            healthComponent.SetHealth(session.Data.hp);
            healthComponent.SetMaxHealth(session.Data.maxHp);
            UpdateInventoryUI();
            UpdateHeroWeapon();
            ShowWeight();
        }

        private void OnDestroy()
        {
            session.Data.Inventory.OnChanged -= OnInventoryChanged;
        }

        private void OnInventoryChanged(string id, int value)
        {
            UpdateInventoryUI();
            if(id == "Sword")
                UpdateHeroWeapon();
        }

        private void UpdateInventoryUI()
        {
            goldText.text = CoinsCount.ToString();
            swordText.text = SwordCount.ToString();
            healPotionText.text = HealPotionCount.ToString();
            ShowWeight();
        }
        

        public bool AddInInventory(string id, int value)
        {
            var itemDef = DefsFacade.I.Items.Get(id);
            if(itemDef.IsVoid) return false;
            
            var itemWeight = itemDef.Weight * value;
            var weight = session.Data.Inventory.WeightCount();
            
            if(weight + itemWeight > maxWeight) return false;
            if (id == "Sword" && SwordCount + value > maxSwords) return false;
            
            session.Data.Inventory.Add(id, value);
            
            return true;
        }
        
        private void ShowWeight()
        {
            if(!weightText) return; 
            weightText.text = $"Weight: {session.Data.Inventory.WeightCount()} | {maxWeight.ToString()}";
        }

        public int GetCoinsValue()
        {
            return CoinsCount;
        }

        public void CollectGold(int amount)
        {
            session.Data.Inventory.Add("Coin", amount);
        }
        
        public bool TryCollectSword(int amount)
        {
            if (SwordCount + amount > maxSwords) return false;
            session.Data.Inventory.Add("Sword", amount);
            
            return true;
        }
        
        public void OnHealthChanged(int health)
        {
            session.Data.hp = health;
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
            var numSwords = session.Data.Inventory.Count("Sword");
            if(isLastSwordThrowing) return;
            Animator.runtimeAnimatorController = SwordCount > 0 ? armed : unarmed;
        }

        public void OnDoThrow()
        {
            Sounds.Play("Range");
            particles.Spawn("Throw");
            isLastSwordThrowing = false;
            UpdateHeroWeapon();
        }
        

        public void Throw()
        {
            if (throwCooldown.IsReady && SwordCount > 0)
            {
                Animator.SetTrigger(ThrowKey);
                if(SwordCount == 1)
                    isLastSwordThrowing = true;
                
                session.Data.Inventory.Remove("Sword", 1);
                
                throwCooldown.Reset();
            }
        }

        public void UsePotion()
        {
            var itemsCount = session.Data.Inventory.Count("HealPotion");
            if (itemsCount <= 0) return;
            session.Data.Inventory.Remove("HealPotion", 1);
            healthComponent.TryHeal(1);
        }
    }
}