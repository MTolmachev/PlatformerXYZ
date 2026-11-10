using System.Collections;
using Components.ColliderBased;
using Components.GOBased;
using Components.Health;
using Creatures.Mobs.Patrolling;
using UnityEngine;
using UnityEngine.Events;

namespace Creatures.Mobs
{
    [RequireComponent(typeof(Creature), typeof(Animator), typeof(Rigidbody2D))]
    public class ExplosiveMobAI : MonoBehaviour
    {
        [Header("Detection")]
        [SerializeField] private LayerCheck vision;
        [SerializeField] private LayerCheck explosionProximity;
        [SerializeField] private float alarmDelay = 0.5f;
        [SerializeField] private float lostTargetDelay = 0.5f;
        [SerializeField] private float chaseSpeedMultiplier = 2f;
        [SerializeField] private float chaseAcceleration = 4f;

        [Header("Explosion")]
        [SerializeField] private AreaDamageComponent areaDamage;
        [SerializeField] private float proximityFuseDelay = 1f;
        [SerializeField] private float deathFuseDelay = 0.3f;
        [SerializeField] private float destroyDelay = 0.5f;

        [Header("Events")]
        [SerializeField] private UnityEvent onFuseStarted;
        [SerializeField] private UnityEvent onExploded;

        private static readonly int IsChasing = Animator.StringToHash("isChasing");
        private static readonly int IsFusing = Animator.StringToHash("isFusing");
        private static readonly int Explode = Animator.StringToHash("explode");

        private State state;
        private Coroutine currentState;
        private GameObject target;
        private float explosionTime;
        private float currentSpeedMultiplier = 1f;

        private Creature creature;
        private Patrol patrol;
        private SpawnListComponent particles;
        private ExplosiveMobBlink blink;
        private ExplosionWaveEffect explosionWave;
        private Animator animator;
        private Rigidbody2D rb;
        private Collider2D[] colliders;

        private enum State
        {
            Patrol,
            Alert,
            Chase,
            Lost,
            Fuse,
            Exploded
        }

        private void Awake()
        {
            creature = GetComponent<Creature>();
            patrol = GetComponent<Patrol>();
            particles = GetComponent<SpawnListComponent>();
            blink = GetComponent<ExplosiveMobBlink>();
            explosionWave = GetComponent<ExplosionWaveEffect>();
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            colliders = GetComponentsInChildren<Collider2D>();
        }

        private void Start()
        {
            if (!ValidateConfiguration())
            {
                enabled = false;
                return;
            }

            StartPatrol();
        }

        public void OnHeroInVision(GameObject hero)
        {
            if (state != State.Patrol || hero == null)
                return;

            var targetCreature = hero.GetComponentInParent<Creature>();
            target = targetCreature != null ? targetCreature.gameObject : hero;

            StartState(State.Alert, Alert());
        }

        private IEnumerator Alert()
        {
            LookAtTarget();
            particles.Spawn("Exclamation");

            yield return new WaitForSeconds(alarmDelay);

            if (state != State.Alert)
                yield break;

            if (target == null)
            {
                StartPatrol();
                yield break;
            }

            if (!vision.IsTouchingLayer)
            {
                StartState(State.Lost, LostTarget());
                yield break;
            }

            StartState(State.Chase, Chase());
        }

        private void LookAtTarget()
        {
            creature.SetDirection(Vector2.zero);
            creature.UpdateSpriteDirection(GetDirectionToTarget());
        }
        
        public void OnDie()
        {
            BeginExplosion(deathFuseDelay);
        }

        private IEnumerator Chase()
        {
            var isFirstIteration = true;

            while (state == State.Chase && target != null)
            {
                if (explosionProximity.IsTouchingLayer)
                {
                    BeginExplosion(proximityFuseDelay);
                    yield break;
                }

                if (!isFirstIteration && !vision.IsTouchingLayer)
                    break;

                currentSpeedMultiplier = Mathf.MoveTowards(
                    currentSpeedMultiplier,
                    chaseSpeedMultiplier,
                    chaseAcceleration * Time.fixedDeltaTime);
                creature.SetSpeedMultiplier(currentSpeedMultiplier);
                SetDirectionToTarget();
                isFirstIteration = false;
                yield return new WaitForFixedUpdate();
            }

            if (state == State.Chase)
                StartState(State.Lost, LostTarget());
        }

        private IEnumerator LostTarget()
        {
            particles.Spawn("Miss");

            yield return new WaitForSeconds(lostTargetDelay);

            if (state == State.Lost)
                StartPatrol();
        }

        private void SetDirectionToTarget()
        {
            creature.SetDirection(GetDirectionToTarget());
        }

        private Vector2 GetDirectionToTarget()
        {
            var direction = target.transform.position - transform.position;
            direction.y = 0f;
            return direction.normalized;
        }

        private void StartPatrol()
        {
            target = null;
            StartState(State.Patrol, patrol.DoPatrol());
        }

        private void BeginExplosion(float delay)
        {
            if (state == State.Exploded)
                return;

            var requestedExplosionTime = Time.time + Mathf.Max(0f, delay);

            if (state == State.Fuse)
            {
                explosionTime = Mathf.Min(explosionTime, requestedExplosionTime);
                return;
            }

            explosionTime = requestedExplosionTime;
            StartState(State.Fuse, Fuse());
            onFuseStarted?.Invoke();
        }

        private IEnumerator Fuse()
        {
            while (state == State.Fuse && Time.time < explosionTime)
            {
                blink.SetFuseBlink(explosionTime - Time.time);
                yield return null;
            }

            if (state == State.Fuse)
                StartState(State.Exploded, Explosion());
        }

        private IEnumerator Explosion()
        {
            animator.SetTrigger(Explode);

            explosionWave.Play(areaDamage.Center, areaDamage.Radius);
            areaDamage.ApplyDamage(gameObject);
            onExploded?.Invoke();

            foreach (var currentCollider in colliders)
            {
                if (currentCollider != null)
                    currentCollider.enabled = false;
            }

            rb.velocity = Vector2.zero;
            rb.simulated = false;

            if (destroyDelay > 0f)
                yield return new WaitForSeconds(destroyDelay);

            Destroy(gameObject);
        }

        private void StartState(State nextState, IEnumerator routine)
        {
            creature.SetDirection(Vector2.zero);

            if (nextState != State.Chase)
            {
                currentSpeedMultiplier = 1f;
                creature.SetSpeedMultiplier(currentSpeedMultiplier);
            }

            if (currentState != null)
                StopCoroutine(currentState);

            state = nextState;
            UpdateAnimatorState();
            currentState = StartCoroutine(routine);
        }

        private void UpdateAnimatorState()
        {
            animator.SetBool(IsChasing, state == State.Chase);
            animator.SetBool(IsFusing, state == State.Fuse);

            if (state == State.Chase)
                blink.SetChaseBlink();
            else if (state == State.Fuse)
                blink.SetFuseBlink(explosionTime - Time.time);
            else
                blink.ResetBlinkSpeed();
        }

        private bool ValidateConfiguration()
        {
            if (patrol == null)
            {
                Debug.LogError("ExplosiveMobAI requires a Patrol component.", this);
                return false;
            }

            if (vision == null || explosionProximity == null || areaDamage == null ||
                particles == null || blink == null || explosionWave == null)
            {
                Debug.LogError("ExplosiveMobAI has unassigned references.", this);
                return false;
            }

            return true;
        }

        private void OnDisable()
        {
            if (creature != null)
                creature.SetDirection(Vector2.zero);
        }

        private void OnValidate()
        {
            alarmDelay = Mathf.Max(0f, alarmDelay);
            lostTargetDelay = Mathf.Max(0f, lostTargetDelay);
            chaseSpeedMultiplier = Mathf.Max(1f, chaseSpeedMultiplier);
            chaseAcceleration = Mathf.Max(0f, chaseAcceleration);
            proximityFuseDelay = Mathf.Max(0f, proximityFuseDelay);
            deathFuseDelay = Mathf.Max(0f, deathFuseDelay);
            destroyDelay = Mathf.Max(0f, destroyDelay);
        }
    }
}
