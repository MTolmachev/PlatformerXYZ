using Components.Health;
using UnityEngine;
namespace Components.Collectables
{
    public class HealthPotionCollectableComponent : CollectObjectComponent
    {
        [Min(1)]
        [SerializeField] private int amount;
        
        protected override bool TryCollect(GameObject collector)
        {
            if(!collector.TryGetComponent<HealthComponent>(out var health))
                return false;
            return health.TryHeal(amount);
        }
    }
}