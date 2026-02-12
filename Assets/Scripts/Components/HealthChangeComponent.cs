using System;
using Character;
using UnityEngine;

namespace Components
{
    public class HealthChangeComponent : MonoBehaviour
    {
        [SerializeField] private int amount;
        [SerializeField] private ChangeHealthType type;
        [SerializeField] private GameObject target;

        public void ApplyHealthChange()
        {
            var healthComponent = target.GetComponent<HealthComponent>();
            if (healthComponent == null) return;
            switch (type)
            {
                case ChangeHealthType.Damage:
                    healthComponent.TakeDamage(amount);
                    break;
                case ChangeHealthType.Heal:
                    if(healthComponent.CurrentHealth == healthComponent.GetMaxHealth()) break;
                    healthComponent.TakeHeal(amount);
                    Destroy(gameObject);
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
