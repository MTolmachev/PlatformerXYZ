using UnityEngine;

namespace Creatures.Mobs
{
    [RequireComponent(typeof(Animator))]
    public class ExplosiveMobBlink : MonoBehaviour
    {
        [Header("Chase")]
        [SerializeField] private float chaseBlinkSpeed = 1f;

        [Header("Explosion")]
        [SerializeField] private float fuseStartBlinkSpeed = 1f;
        [SerializeField] private float fuseEndBlinkSpeed = 4f;
        [SerializeField] private float fuseAccelerationWindow = 1f;

        private static readonly int BlinkSpeed = Animator.StringToHash("blinkSpeed");

        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void SetChaseBlink()
        {
            SetBlinkSpeed(chaseBlinkSpeed);
        }

        public void SetFuseBlink(float timeRemaining)
        {
            var progress = fuseAccelerationWindow > 0f
                ? 1f - Mathf.Clamp01(timeRemaining / fuseAccelerationWindow)
                : 1f;

            SetBlinkSpeed(Mathf.Lerp(fuseStartBlinkSpeed, fuseEndBlinkSpeed, progress));
        }

        public void ResetBlinkSpeed()
        {
            SetBlinkSpeed(1f);
        }

        private void SetBlinkSpeed(float speed)
        {
            animator.SetFloat(BlinkSpeed, Mathf.Max(0.01f, speed));
        }

        private void OnValidate()
        {
            chaseBlinkSpeed = Mathf.Max(0.01f, chaseBlinkSpeed);
            fuseStartBlinkSpeed = Mathf.Max(0.01f, fuseStartBlinkSpeed);
            fuseEndBlinkSpeed = Mathf.Max(fuseStartBlinkSpeed, fuseEndBlinkSpeed);
            fuseAccelerationWindow = Mathf.Max(0f, fuseAccelerationWindow);
        }
    }
}
