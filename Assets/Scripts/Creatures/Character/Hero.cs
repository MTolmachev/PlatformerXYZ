using System;
using System.Collections;
using Components;
using Components.ColliderBased;
using Components.GOBased;
using Components.Health;
using Model;
using Model.Data;
using Model.Definitions;
using Model.Definitions.Repositories;
using Model.Definitions.Repositories.Items;
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

        [Header("Triple Throw")]
        [SerializeField] private Cooldown tripleThrowHoldCooldown;
        [SerializeField] private int tripleThrowCount;
        [SerializeField] private float tripleThrowDelay;

        [SerializeField] private SpawnComponent throwSpawner;
        
        private bool canDoubleJump;
        private bool isLastSwordThrowing;
        private bool isTripleThrow;

        private readonly Cooldown speedUpCooldown = new Cooldown();
        private float additionalSpeed;
        
        private GameSession session;
        private HealthComponent healthComponent;

        private const string SwordId = "Sword";
        
        private int SwordCount => session.Data.Inventory.Count(SwordId);
        private int CoinsCount => session.Data.Inventory.Count("Coin");

        private bool CanThrow
        {
            get
            {
                if (!session.QuickInventory.TryGetSelectedItem(out var selectedItem))
                    return false;

                if (selectedItem.Id == SwordId)
                    return SwordCount > 1;

                var def = DefsFacade.I.Items.Get(selectedItem.Id);
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
        

        public bool TryAddInInventory(string id, int value)
        {
            return session.Data.Inventory.Add(id, value);
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
            else if (canDoubleJump && session.PerksModel.TryUse("double-jump"))
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
            if (!session.QuickInventory.TryGetSelectedItem(out var selectedItem)) return;

            var throwableId = selectedItem.Id;
            if (isTripleThrow && session.PerksModel.TryUse("super-throw"))
            {
                var throwableCount = session.Data.Inventory.Count(throwableId);
                var availableToThrow = throwableId == SwordId ? throwableCount - 1 : throwableCount;
                var throwsCount = Mathf.Min(tripleThrowCount, availableToThrow);

                StartCoroutine(DoTripleThrow(throwableId, throwsCount));
            }
            else
            {
                ThrowAndRemoveFromInventory(throwableId);
            }

            isTripleThrow = false;
        }

        
        private IEnumerator DoTripleThrow(string throwableId, int throwsCount)
        {
            for (var i = 0; i < throwsCount; i++)
            {
                ThrowAndRemoveFromInventory(throwableId);
                yield return new WaitForSeconds(tripleThrowDelay);
            }
        }

        private void ThrowAndRemoveFromInventory(string throwableId)
        {
            Sounds.Play("Range");
            var throwableDef = DefsFacade.I.Throwable.Get(throwableId);
            
            throwSpawner.SetPrefab(throwableDef.Projectile);
            throwSpawner.Spawn();
            
            session.Data.Inventory.Remove(throwableId, 1);
            
            isLastSwordThrowing = false;
            UpdateHeroWeapon();
        }

        public void StartThrowing()
        {
            tripleThrowHoldCooldown.Reset();
        }

        public void Throw()
        {
            if (!throwCooldown.IsReady || !CanThrow) return;

            isTripleThrow = tripleThrowHoldCooldown.IsReady;
            Animator.SetTrigger(ThrowKey);
                
            throwCooldown.Reset();
        }

        public void UseInventory()
        {
            if (IsSelectedItem(ItemTag.Throwable))
                Throw();
            else if (IsSelectedItem(ItemTag.Potion))
                UsePotion();
        }

        public void UsePotion()
        {
            if (!session.QuickInventory.TryGetSelectedItem(out var selectedItem)) return;

            var potion = DefsFacade.I.Potions.Get(selectedItem.Id);
            if (potion.IsVoid) return;

            switch (potion.Effect)
            {
                case Effect.AddHp:
                    if (!healthComponent.TryHeal((int) potion.Value))
                        return;
                    break;
                case Effect.SpeedUp:
                    if (potion.Value <= 0f || potion.Time <= 0f)
                        return;

                    speedUpCooldown.Value = speedUpCooldown.TimeLasts + potion.Time;
                    additionalSpeed = Mathf.Max(potion.Value, additionalSpeed);
                    speedUpCooldown.Reset();
                    break;
                default:
                    return;
            }

            session.Data.Inventory.Remove(potion.Id, 1);
        }

        protected override float CalculateSpeed()
        {
            if (speedUpCooldown.IsReady)
                additionalSpeed = 0f;

            return base.CalculateSpeed() + additionalSpeed;
        }

        private bool IsSelectedItem(ItemTag tag)
        {
            return session != null && session.QuickInventory.SelectedDef.HasTag(tag);
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
