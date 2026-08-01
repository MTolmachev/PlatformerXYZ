using System;

namespace Model
{
    [Serializable]
    public class PlayerData
    {
        public int maxHp;
        public int hp;
        public int coins;
        public bool isArmed;

        public PlayerData Clone()
        {
            return new PlayerData
            {
                maxHp = this.maxHp,
                hp = this.hp,
                coins = this.coins,
                isArmed = this.isArmed
            };
        }
    }
}