using System;
using UnityEngine;

namespace Utils
{
    [Serializable]
    public class Cooldown
    {
        [SerializeField] private float value;

        private float timesUp;
        public bool IsReady => timesUp <= Time.time;
        
        public void Reset()
        {
            timesUp = Time.time + value;
        }
        

    }
}