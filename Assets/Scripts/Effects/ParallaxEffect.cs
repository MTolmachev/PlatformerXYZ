using System;
using UnityEngine;

namespace Effects
{
    public class ParallaxEffect : MonoBehaviour
    {
        [SerializeField] private float effectValue;
        [SerializeField] private Transform followTarget;
        private float startX;

        private void Start()
        {
            startX = transform.position.x;
        }

        private void LateUpdate()
        {
            var currentPosition = transform.position;
            var deltaX = followTarget.position.x * effectValue;
            transform.position = new Vector3(startX + deltaX, currentPosition.y, currentPosition.z);
        }
    }
}