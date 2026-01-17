using UnityEngine;
using Character;

namespace Components
{
    public class CollectObjectComponent : MonoBehaviour
    {
        [SerializeField] private int cost;
        [SerializeField] private Hero character;
        public void Collect()
        {
            character.CollectGold(cost);
        }
    }
}
