using System;
using UnityEngine.Serialization;

namespace Model
{
    [Serializable]
    public class PlayerData
    {
        public int coins;
        public int hp;
        public bool isArmed;

        public PlayerData Clone()
        {
            return new PlayerData
            {
                hp = this.hp,
                coins = this.coins,
                isArmed = this.isArmed
            };
        }
    }
}