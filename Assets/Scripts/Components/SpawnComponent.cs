using UnityEngine;

namespace Components
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private GameObject prefab;

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            var instance = Instantiate(prefab, target.position, Quaternion.identity);
            instance.transform.localScale = target.lossyScale;
        }
    }
}