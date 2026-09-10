using System;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Components.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class VerticalLevitationComponent : MonoBehaviour
    {
        [SerializeField] private float frequency = 1f;
        [SerializeField] private float amplitude = 1f;
        [SerializeField] private bool randomize;
        
        private float originalY;
        private Rigidbody2D rb;
        private float seed;

        private void Awake()
        {
            rb =  GetComponent<Rigidbody2D>();
            originalY = rb.position.y;
            if(randomize)
                seed = Random.value * Mathf.PI * 2;
        }

        private void Update()
        {
            var pos = rb.position;
            pos.y = originalY + Mathf.Sin(seed + Time.time * frequency) * amplitude;
            rb.position = pos;
        }
    }
}