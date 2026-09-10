using System;
using UnityEngine;

namespace Components
{
    public class ModifyHealthComponent : MonoBehaviour
    {
        [SerializeField] private int amount;
        [SerializeField] private ChangeHealthType type;

        public void ApplyHealthChange(GameObject target)
        {
            var healthComponent = target.GetComponentInParent<HealthComponent>();
            if (healthComponent == null) return;
            switch (type)
            {
                case ChangeHealthType.Damage:
                    healthComponent.TakeDamage(amount);
                    break;
                case ChangeHealthType.Heal:
                    healthComponent.TryHeal(amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private enum ChangeHealthType
        {
            Damage,
            Heal
        }
    }

    
}
