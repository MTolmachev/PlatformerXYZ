using Creatures;
using Creatures.Character;
using UnityEngine;

namespace Components.Collectables
{
    public class ArmHeroComponent : MonoBehaviour
    {
        public void ArmHero(GameObject go)
        {
            var hero =  go.GetComponent<Hero>();
            if (hero != null)
                hero.ArmHero();
        }
    }
}