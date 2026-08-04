using Creatures;
using Creatures.Character;
using UnityEngine;

namespace Components.Collectables
{
    public class SwordCollectableComponent : CollectObjectComponent
    {
        [Min((1))]
        [SerializeField] private int amount;


        protected override bool TryCollect(GameObject collector)
        {
            if(!collector.TryGetComponent<Hero>(out var hero))
                return false;
            return hero.TryCollectSword(amount);
        }
    }
}