using UnityEngine;

namespace Model.Definitions
{
    [CreateAssetMenu(fileName = "Defs/PlayerDef", menuName = "PlayerDef", order = 0)]
    public class PlayerDef : ScriptableObject
    {
        [SerializeField] private int maxHealth;
        
        public int MaxHealth => maxHealth;
    }
}