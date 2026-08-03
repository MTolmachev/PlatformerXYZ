using UnityEngine;
using UnityEngine.Serialization;

namespace Components
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
