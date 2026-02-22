using UnityEngine;

namespace Components
{
    public class ShipMovementComponent : MonoBehaviour
    {
        [SerializeField] private GameObject child;
        
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject == child)
            {
                collision.transform.SetParent(transform);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject == child)
            {
                collision.transform.SetParent(null);
            }
        }
    }
}