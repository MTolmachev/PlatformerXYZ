using Components;
using UnityEngine;

namespace Creatures
{
    public class Creature : MonoBehaviour
    {
        [Header("Params")] [SerializeField] private bool invertScale;
        [SerializeField] private float speed;
        [SerializeField] protected float jumpForce;
        [SerializeField] protected float damageJumpForce;
/*
        [SerializeField] private int damage;
*/
        
        [Header("Checkers")]
        [SerializeField] private LayerCheck layerCheck;
        [SerializeField] protected CheckCircleOverlap attackRange;
        
        [Header("Particles")]
        [SerializeField] protected SpawnListComponent particles;

        private Vector2 direction;
        protected Rigidbody2D Rb;
        protected Animator Animator;

        private static readonly int IsRunning = Animator.StringToHash("isRunning");
        private static readonly int VerticalVelocity = Animator.StringToHash("verticalVelocity");
        private static readonly int Grounded = Animator.StringToHash("isGrounded");
        private static readonly int Hit = Animator.StringToHash("isHit");
        private static readonly int Attacking = Animator.StringToHash("attack");

        protected virtual void Awake()
        {
            Rb = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>(); 
        }
        
        public void SetDirection(Vector2 dir)
        {
            this.direction = dir;
        }
        
        protected bool IsGrounded()
        {
            return layerCheck.IsTouchingLayer;
        }
        
        private void FixedUpdate()
        {
            Rb.velocity = new Vector2(direction.x * speed, Rb.velocity.y);

            UpdateSpriteDirection();
            
            Animator.SetBool(Grounded, IsGrounded());
            Animator.SetFloat(VerticalVelocity, Rb.velocity.y);
            Animator.SetBool(IsRunning, direction.x != 0);
        }
        
        private void UpdateSpriteDirection()
        {
            var multiplier = invertScale ? -1 : 1;
            if(direction.x > 0)
                transform.localScale = new Vector3(multiplier, 1, 1);
            else if(direction.x < 0)
                transform.localScale = new Vector3(-1 * multiplier, 1, 1);
        }
        
        public virtual void TakeDamage()
        {
            Animator.SetTrigger(Hit);
            Rb.velocity = new Vector2(Rb.velocity.x, damageJumpForce);
            
        }
        
        public virtual void Attack()
        {
            Animator.SetTrigger(Attacking);
        }
        
        public void OnAttack()
        {
            attackRange.Check();
        }
    }
}