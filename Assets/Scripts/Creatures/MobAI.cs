using System.Collections;
using Components;
using UnityEngine;

namespace Creatures
{
    public class MobAI  : MonoBehaviour
    {
        [SerializeField] private LayerCheck vision;
        [SerializeField] private LayerCheck canAttack;

        [SerializeField] private float alarmDelay = 0.5f;
        [SerializeField] private float attackCooldown = 1f;
        [SerializeField] private float missCooldown = 0.5f;

        private Coroutine current;
        private GameObject target;

        private SpawnListComponent particles;
        private Creature creature;
        private Animator animator;
        private Patrol patrol;
        
        private bool isDead;

        private static readonly int Dead = Animator.StringToHash("isDead");
        

        private void Awake()
        {
            particles = GetComponent<SpawnListComponent>();
            creature =  GetComponent<Creature>();
            animator = GetComponent<Animator>();
            patrol = GetComponent<Patrol>();
            
        }

        private void Start()
        {
            StartState(patrol.DoPatrol());

        }

        public void OnHeroInVision(GameObject go)
        {
            if (isDead) return;
            
            target = go;
            
            StartState(AgroToHero());
        }

        private IEnumerator AgroToHero()
        {
            particles.Spawn("Exclamation");
            yield return new WaitForSeconds(alarmDelay);
            StartState(GoToHero());
        }

        private IEnumerator GoToHero()
        {
            while (vision.IsTouchingLayer)
            {
                if (canAttack.IsTouchingLayer)
                {
                    StartState(Attack());      
                }
                else
                {
                    SetDirectionToTarget();
                }
                yield return null;
            }
            particles.Spawn("Miss");
            yield return new WaitForSeconds(missCooldown);
            StartState(patrol.DoPatrol());
            
        }

        private IEnumerator Attack()
        {
            while (canAttack.IsTouchingLayer)
            {
                creature.Attack();
                yield return new WaitForSeconds(attackCooldown);
            }
            
            StartState(GoToHero());
        }

        private void SetDirectionToTarget()
        {
            var direction = target.transform.position - transform.position;
            direction.y = 0;
            creature.SetDirection(direction.normalized);
            
        }

        private void StartState(IEnumerator coroutine)
        {
            creature.SetDirection(Vector2.zero);
            if(current != null)
                StopCoroutine(current);
            
            current = StartCoroutine(coroutine);
        }

        public void OnDie()
        {
            isDead = true;
            animator.SetBool(Dead, true);
            creature.SetDirection(Vector2.zero);
            
            if(current != null)
                StopCoroutine(current);
        }
    }
}