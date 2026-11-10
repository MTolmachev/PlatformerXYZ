using System.Collections.Generic;
using UnityEngine;

namespace Components.Health
{
    public class AreaDamageComponent : MonoBehaviour
    {
        [SerializeField] private Transform center;
        [SerializeField] private float radius = 2f;
        [SerializeField] private LayerMask targets;
        [SerializeField] private int damage = 1;

        private readonly Collider2D[] overlapResults = new Collider2D[32];
        private readonly HashSet<HealthComponent> damagedTargets = new HashSet<HealthComponent>();
        private bool wasApplied;

        public Vector3 Center => center != null ? center.position : transform.position;
        public float Radius => radius;
        
        public void ApplyDamage(GameObject source)
        {
            if (wasApplied || damage <= 0) return;
            wasApplied = true;

            var sourceHealth = source != null
                ? source.GetComponentInParent<HealthComponent>()
                : null;

            var count = Physics2D.OverlapCircleNonAlloc(Center, radius, overlapResults, targets);

            damagedTargets.Clear();

            for (var i = 0; i < count; i++)
            {
                var targetHealth = overlapResults[i].GetComponentInParent<HealthComponent>();
                if (targetHealth == null || targetHealth == sourceHealth)
                    continue;
                if (!damagedTargets.Add(targetHealth))
                    continue;
                targetHealth.TakeDamage(damage);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1f, 0.25f, 0.1f, 0.35f);
            Gizmos.DrawSphere(Center, radius);
            Gizmos.color = new Color(1f, 0.25f, 0.1f, 1f);
            Gizmos.DrawWireSphere(Center, radius);
        }

        private void OnValidate()
        {
            radius = Mathf.Max(0f, radius);
            damage = Mathf.Max(0, damage);
        }
    }
}
