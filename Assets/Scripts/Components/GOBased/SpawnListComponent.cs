using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Components.GOBased
{
    public class SpawnListComponent : MonoBehaviour
    {
        [SerializeField] private SpawnData[] spawners;

        public void Spawn(string id)
        {
            var spawner = spawners.FirstOrDefault(element => element.id == id);
            spawner?.component.Spawn();
        }

        [Serializable]
        public class SpawnData
        {
            [FormerlySerializedAs("Id")]
            public string id;

            [FormerlySerializedAs("Component")]
            public SpawnComponent component;
        }
    }
}
