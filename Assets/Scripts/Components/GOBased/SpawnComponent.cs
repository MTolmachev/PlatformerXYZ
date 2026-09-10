using UnityEngine;

namespace Components.GOBased
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private GameObject prefab;
        [SerializeField] private bool inParent = false;

        [ContextMenu("Spawn")]
        public void Spawn()
        {
            var instance = Instantiate(prefab, target.position, Quaternion.identity);
            instance.transform.localScale = target.lossyScale;
            if (inParent)
                instance.transform.SetParent(target);
        }
    }
}