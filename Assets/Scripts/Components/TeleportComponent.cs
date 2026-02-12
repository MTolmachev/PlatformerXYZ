using UnityEngine;

namespace Components
{
    public class TeleportComponent : MonoBehaviour
    {
        [SerializeField] private Transform destinationPosition;

        public void Teleport(GameObject target)
        {
            target.transform.position = destinationPosition.position;
        }
    }
}