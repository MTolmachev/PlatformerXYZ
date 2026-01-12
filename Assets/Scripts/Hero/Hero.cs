using System;
using UnityEditor.UIElements;
using UnityEngine;

namespace Hero
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private float speed;
        
        private float direction;
      
        public void SetDirection(float dir)
        {
            this.direction = dir;
        }

        public void SaySomething()
        {
            Debug.Log("SaySomething");
        }

        private void Update()
        {
            if (direction != 0)
            {
                var delta = direction * speed * Time.deltaTime;
                var newXPosition = transform.position.x + delta;
                transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
            }
                
        }
    }
}