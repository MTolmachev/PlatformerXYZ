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

        public float Value
        {
            get => value;
            set => this.value = value;
        }

        public float TimeLasts => Mathf.Max(timesUp - Time.time, 0f);
        
        public void Reset()
        {
            timesUp = Time.time + value;
        }
        

    }
}
