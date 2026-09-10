using System;
using Components.ColliderBased;
using Components.GOBased;
using UnityEngine;
using Utils;

namespace Creatures.Mobs
{
    public class ShootingTrapAI :  MonoBehaviour
    {
        [SerializeField] private LayerCheck vision;
        
        [Header("Melee")]
        [SerializeField] private CheckCircleOverlap meleeAttack;
        [SerializeField] private LayerCheck meleeCanAttack;
        [SerializeField] private Cooldown meleeCooldown;
        
        [Header("Range")]
        [SerializeField] private SpawnComponent rangeAttack;
        [SerializeField] private Cooldown rangeCooldown;

        private Animator animator;

        private static readonly int Melee = Animator.StringToHash("melee");
        private static readonly int Range = Animator.StringToHash("range");
        
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (!vision.IsTouchingLayer) return;
            if (meleeCanAttack.IsTouchingLayer)
            {
                if (meleeCooldown.IsReady)
                    MeleeAttack();
                return;
            }

            if (rangeCooldown.IsReady)
            {
                RangeAttack();
            }
        }

        private void RangeAttack()
        {
            rangeCooldown.Reset();
            animator.SetTrigger(Range);
        }

        private void MeleeAttack()
        {
            meleeCooldown.Reset();
            animator.SetTrigger(Melee);
        }

        public void OnMeleeAttack()
        {
            meleeAttack.Check();
        }
        
        public void OnRangeAttack()
        {
            rangeAttack.Spawn();
        }
    }
}