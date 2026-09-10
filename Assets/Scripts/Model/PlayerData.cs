using System;

namespace Model
{
    [Serializable]
    public class PlayerData
    {
        public int maxHp;
        public int hp;
        public int coins;
        public int swords;
        public bool isArmed;

        public PlayerData Clone()
        {
            return new PlayerData
            {
                maxHp = this.maxHp,
                hp = this.hp,
                coins = this.coins,
                swords = this.swords,
                isArmed = this.isArmed
            };
        }
    }
}