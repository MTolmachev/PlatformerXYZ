using UnityEngine;

namespace Components.GOBased
{
    public class DestroyObjectComponent : MonoBehaviour
    {
        [SerializeField] private GameObject destroyableObject;
        
        public void DestroyObject()
        {
            Destroy(destroyableObject);
        }
    }
}
