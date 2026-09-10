using UnityEngine;

namespace Creatures.Weapons
{
    public abstract class BaseProjectile : MonoBehaviour
    {
        [SerializeField] protected float speed;
        [SerializeField] private bool invertX;

        protected Rigidbody2D Rb;
        protected int Direction;

        protected virtual void Start()
        {
            var mod = invertX ? -1 : 1;
            Direction = mod * transform.lossyScale.x > 0 ? 1 : -1;
            Rb = GetComponent<Rigidbody2D>();
        }
    }
}